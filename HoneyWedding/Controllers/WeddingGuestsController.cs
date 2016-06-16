using HoneyWedding.DAL.Repositories;
using HoneyWedding.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using Omu.ValueInjecter;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HoneyWedding.Controllers
{
    public class WeddingGuestsController : BaseController
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: WeddingGuests
        [Authorize(Roles = "WeddingAdmin")]
        public async Task<ActionResult> Index()
        {
            var guests = await _unitOfWork.WeddingGuestRepository.GetAsync(null, q => q.OrderBy(g => g.LastName));
            return View(guests);
        }

        [Authorize(Roles = "WeddingAdmin")]
        public ActionResult Invite()
        {
            return View();
        }

        [Authorize(Roles = "WeddingAdmin")]
        // POST: /WeddingGuests/Create
        [HttpPost]
        public async Task<ActionResult> Invite(InviteWeddingGuestViewModel guestViewModel)
        {
            InviteWeddingGuestViewModel viewModel = new InviteWeddingGuestViewModel();

            if (ModelState.IsValid)
            {
                var newUser = new WeddingGuest
                {
                    FirstName = guestViewModel.FirstName,
                    LastName = guestViewModel.LastName,
                    UserName = guestViewModel.Email,
                    Email = guestViewModel.Email,
                    PhoneNumber = guestViewModel.PhoneNumber,
                    IsActive = true,
                    InviteDate = DateTime.Now,
                    HasPlusOne = guestViewModel.HasPlusOne,
                    Notes = guestViewModel.Notes
                };
                // set guest's password to their email address
                string password = guestViewModel.Email;
                var adminresult = await UserManager.CreateAsync(newUser, password);
                string requestMessage = "";

                if (adminresult.Succeeded)
                {
                    // send invitation email to guest
                    string body;
                    string templateTail;
                    string plusOneMessage = "<h4>But wait, there's more... you even get a <i>PLUS ONE!</i> (so choose your guest wisely...)</h4>";
                    using (var sr = new StreamReader(Server.MapPath("\\Templates\\") + "InviteWeddingGuestEmailHead.html"))
                    {
                        body = await sr.ReadToEndAsync();
                    }
                    using (var sr = new StreamReader(Server.MapPath("\\Templates\\") + "InviteWeddingGuestEmailTail.html"))
                    {
                        templateTail = await sr.ReadToEndAsync();
                    }
                    if (newUser.HasPlusOne)
                    {
                        body += plusOneMessage;
                    }
                    body += templateTail;
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(newUser.Id);
                    var callbackUrl = Url.Action("RSVP", "WeddingGuests", new { userId = newUser.Id }, protocol: Request.Url.Scheme);
                    string messageBody = string.Format(body, newUser.FirstName, callbackUrl);
                    await UserManager.SendEmailAsync(newUser.Id, "Please RSVP--You're invited to Russ and Lisa's Wedding!", messageBody);

                    //Add User to the selected Roles 
                    var result = await UserManager.AddToRoleAsync(newUser.Id, "WeddingGuest");
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                        return View(viewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                    return View(viewModel);

                }
                Success("Wedding Guest was invited successfully. " + requestMessage, true);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [Authorize(Roles = "WeddingAdmin")]
        public async Task<ActionResult> Edit(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var guest = await UserManager.FindByIdAsync(userId) as WeddingGuest;

            if (guest == null)
            {
                return HttpNotFound();
            }

            var viewModel = new WeddingGuestViewModel();
            viewModel.InjectFrom(guest);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "WeddingAdmin")]
        public async Task<ActionResult> Edit(WeddingGuestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guest = await _unitOfWork.WeddingGuestRepository.GetByIDAsync(model.Id);
                guest.InjectFrom(model);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("RSVPConfirmation", new { id = model.Id });
            }
            return View(model);
        }

        public async Task<ActionResult> RSVP(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var guest = await UserManager.FindByIdAsync(userId) as WeddingGuest;

            if (guest == null)
            {
                return HttpNotFound();
            }

            var viewModel = new WeddingGuestViewModel();
            viewModel.InjectFrom(guest);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> RSVP(WeddingGuestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guest = await _unitOfWork.WeddingGuestRepository.GetByIDAsync(model.Id);
                guest.InjectFrom(model);
                if (!guest.DidRsvp)
                {
                    guest.DidRsvp = true;
                    guest.RsvpDate = DateTime.Now;
                }
                else
                {
                    guest.UpdatedRsvp = true;
                    guest.UpdatedRsvpDate = DateTime.Now;
                }
                await _unitOfWork.SaveAsync();
                return RedirectToAction("RSVPConfirmation", new { id = model.Id });
            }
            return View(model);
        }

        public ActionResult RSVPConfirmation(string id)
        {
            return View();
        }


    }
}