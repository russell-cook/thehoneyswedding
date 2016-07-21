﻿using HoneyWedding.DAL.Repositories;
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
        public async Task<ActionResult> Invite(InviteWeddingGuestViewModel model)
        {
            InviteWeddingGuestViewModel viewModel = new InviteWeddingGuestViewModel();

            if (ModelState.IsValid)
            {
                var newUser = new WeddingGuest
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    IsActive = true,
                    InviteDate = DateTime.Now,
                    HasPlusOne = model.HasPlusOne,
                    PlusOneIsKnown = model.PlusOneIsKnown,
                    FirstNamePlusOne = model.FirstNamePlusOne,
                    LastNamePlusOne = model.LastNamePlusOne,
                    InviteMessage = model.InviteMessage
                };
                // set guest's password to their email address
                string password = model.Email;
                var adminresult = await UserManager.CreateAsync(newUser, password);
                string requestMessage = "";

                if (adminresult.Succeeded)
                {
                    // send invitation email to guest
                    string body;
                    string salutation;
                    string plusOneMessage = newUser.HasPlusOne && !newUser.PlusOneIsKnown ? "<h4>But wait, there's more... you get to bring a <i>PLUS ONE!</i> (so choose your guest wisely...)</h4>" : "";
                    string specialMessage = string.IsNullOrEmpty(newUser.InviteMessage) ? "" : "<hr /><p><strong>Special Message from The Honeys: </strong>" + newUser.InviteMessage + "</p><hr />";

                    if (newUser.PlusOneIsKnown)
                    {
                        salutation = newUser.FirstName + " and " + newUser.FirstNamePlusOne;
                    }
                    else
                    {
                        salutation = newUser.FirstName;
                    }
                    
                    using (var sr = new StreamReader(Server.MapPath("\\Templates\\") + "InviteWeddingGuestEmailTemplate.html"))
                    {
                        body = await sr.ReadToEndAsync();
                    }
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(newUser.Id);
                    var callbackUrl = Url.Action("RSVP", "WeddingGuests", new { userId = newUser.Id }, protocol: Request.Url.Scheme);
                    string messageBody = string.Format(body, salutation, plusOneMessage, specialMessage, callbackUrl);
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

            var viewModel = new InviteWeddingGuestViewModel();
            viewModel.InjectFrom(guest);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "WeddingAdmin")]
        public async Task<ActionResult> Edit(InviteWeddingGuestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var guest = await _unitOfWork.WeddingGuestRepository.GetByIDAsync(model.Id);
                guest.InjectFrom(model);
                await _unitOfWork.SaveAsync();
                Success("Wedding Guest was updated successfully.", true);
                return RedirectToAction("Index", new { id = model.Id });
            }
            return View(model);
        }

        public async Task<ActionResult> RSVP(string userId)
        {
            if (userId == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                var defaultGuest = await UserManager.FindByEmailAsync("russ.cook@gmail.com") as WeddingGuest;
                var defaultViewModel = new WeddingGuestViewModel();
                defaultViewModel.InjectFrom(defaultGuest);
                return View(defaultViewModel);
            }

            var guest = await UserManager.FindByIdAsync(userId) as WeddingGuest;

            if (guest == null)
            {
                return HttpNotFound();
            }

            if (guest.DidRsvp)
            {
                ViewBag.DidRsvp = true;
                if (guest.CanAttend == false)
                {
                    return RedirectToAction("RsvpNo", new { userId = guest.Id });
                }
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
                    ViewBag.UpdatedRsvp = true;
                }
                await _unitOfWork.SaveAsync();

            // send confirmation email
                string body;
                string greeting = guest.UpdatedRsvp ? "We received your updated RSVP info. " : "Thank you for your RSVP. ";
                if (guest.CanAttend == true)
                {
                    greeting += "We're so glad you'll be able to join us";
                    if (guest.PlusOneCanAtend == false && guest.PlusOneIsKnown)
                    {
                        greeting += string.Format(", but we're sorry {0} won't be able to make it.", guest.FirstNamePlusOne);
                    }
                    else
                    {
                        greeting += ".";
                    }
                }
                else
                {
                    greeting += "We're sorry you won't be able to join us.";
                }

                // format info table
                var attendMessage = string.Empty;
                switch (guest.CanAttend)
                {
                    case true:
                        attendMessage = "Yes";
                        break;
                    case false:
                        attendMessage = "No";
                        break;
                    case null:
                        attendMessage = "Unsure";
                        break;
                    default:
                        break;
                }
                string infoTable = string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >Can you attend?:</ td ><td width = '75%' align = 'left' valign = 'top' >{0}</ td ></ tr >", attendMessage);
                if (guest.CanAttend == true)
                {
                    infoTable += string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >Would you like a meatless option?:</ td ><td width = '75%' align = 'left' valign = 'top' >{0}</ td ></ tr >", guest.Meatless == true ? "Yes" : "No");
                    infoTable += string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >Would you like a vegan option?:</ td ><td width = '75%' align = 'left' valign = 'top' >{0}</ td ></ tr >", guest.Vegan == true ? "Yes" : "No");
                    infoTable += string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >Dietary needs:</ td ><td width = '75%' align = 'left' valign = 'top' >{0}</ td ></ tr >", guest.DietaryNotes);
                    if (guest.HasPlusOne)
                    {
                        var PlusOneAttendMessage = string.Empty;
                        switch (guest.PlusOneCanAtend)
                        {
                            case true:
                                PlusOneAttendMessage = "Yes";
                                break;
                            case false:
                                PlusOneAttendMessage = "No";
                                break;
                            case null:
                                PlusOneAttendMessage = "Unsure";
                                break;
                            default:
                                break;
                        }

                        infoTable += string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >{0}:</ td ><td width = '75%' align = 'left' valign = 'top' >{1}</ td ></ tr >", guest.PlusOneIsKnown ? string.Format("Will {0} be able to attend?", guest.FirstNamePlusOne) : "Would you like to bring a 'plus one'?", PlusOneAttendMessage);
                        if (!guest.PlusOneIsKnown == true && guest.PlusOneCanAtend == true)
                        {
                            infoTable += string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >Name of your 'plus one':</ td ><td width = '75%' align = 'left' valign = 'top' >{0}</ td ></ tr >", guest.FirstNamePlusOne + " " + guest.LastNamePlusOne);
                        }
                        if (guest.PlusOneCanAtend == true)
                        {
                            infoTable += string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >Would {0} like a meatless option?:</ td ><td width = '75%' align = 'left' valign = 'top' >{1}</ td ></ tr >", guest.PlusOneIsKnown ? guest.FirstNamePlusOne : "your 'plus one'", guest.Meatless == true ? "Yes" : "No");
                            infoTable += string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >Would {0} like a vegan option?:</ td ><td width = '75%' align = 'left' valign = 'top' >{1}</ td ></ tr >", guest.PlusOneIsKnown ? guest.FirstNamePlusOne : "your 'plus one'", guest.Vegan == true ? "Yes" : "No");
                            infoTable += string.Format("<tr><td width = '25%' align = 'right' valign = 'top' >Dietary needs for {0}:</ td ><td width = '75%' align = 'left' valign = 'top' >{1}</ td ></ tr >", guest.PlusOneIsKnown ? guest.FirstNamePlusOne : "your 'plus one'", guest.DietaryNotes);
                        }
                    }
                }

                string instructions = guest.CanAttend != false ? "If this information is incorrect, or if you need to change your responses in the future, please use the following link:" : "If your plans change and you'd like to attend, please email Lisa and Russ as soon as possible at <a href = \"mailto:contact@thehoneyswedding.net?subject=Please Change My RSVP\">contact@thehoneyswedding.com</a>, and we'll do our best to accommodate you";
                var callbackUrl = Url.Action("RSVP", "WeddingGuests", new { userId = guest.Id }, protocol: Request.Url.Scheme);
                var button = guest.CanAttend != false ? string.Format("<table border='0' cellpadding='0' cellspacing='12' width='100%'><tr><td align='center'><table border='0' cellpadding='0' cellspacing='0'><tr><td bgcolor='#f46e6c' style='padding: 12px 18px 12px 18px; -webkit-border-radius: 3px; border-radius: 3px; background-color: #ff5858;' align='center'><a href='{0}' style='text-decoration: none; font-size: 18px; color: #ffffff; '>Update RSVP Info</a></td></tr></table></td></tr></table>", callbackUrl) : "";

                using (var sr = new StreamReader(Server.MapPath("\\Templates\\") + "RsvpConfirmationEmailTemplate.html"))
                {
                    body = await sr.ReadToEndAsync();
                }
                string messageBody = string.Format(body, greeting, guest.FirstName, infoTable, instructions, button);
                string subject = guest.UpdatedRsvp ? "Updated RSVP Confirmation--Russ and Lisa's Wedding" : "RSVP Confirmation--Russ and Lisa's Wedding";
                await UserManager.SendEmailAsync(guest.Id, subject, messageBody);

                return RedirectToAction("RSVPConfirmation", new { userId = model.Id });
            }
            return View(model);
        }

        public async Task<ActionResult> RSVPConfirmation(string userId)
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

            if (guest.UpdatedRsvp)
            {
                ViewBag.UpdatedRsvp = true;
            }

            var viewModel = new WeddingGuestViewModel();
            viewModel.InjectFrom(guest);

            return View(viewModel);
        }

        public async Task<ActionResult> RsvpNo(string userId)
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

    }
}