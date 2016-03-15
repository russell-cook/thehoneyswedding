using HoneyWedding.DAL.Repositories;
using HoneyWedding.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HoneyWedding.Controllers
{
    public class WeddingGuestsController : BaseController
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: WeddingGuests
        public async Task<ActionResult> Index()
        {
            var guests = await _unitOfWork.WeddingGuestRepository.GetAsync(null, q => q.OrderBy(g => g.LastName));
            return View(guests);
        }

        [Authorize(Roles = "GlobalAdmin, RolesAdmin, UsersAdmin")]
        public ActionResult Invite()
        {
            return View();
        }

        [Authorize(Roles = "GlobalAdmin, RolesAdmin, UsersAdmin")]
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
                    using (var sr = new StreamReader(Server.MapPath("\\Templates\\") + "InviteWeddingGuestEmail.html"))
                    {
                        body = await sr.ReadToEndAsync();
                    }
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


    }
}