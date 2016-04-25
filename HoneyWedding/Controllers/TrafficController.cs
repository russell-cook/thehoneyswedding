using HoneyWedding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HoneyWedding.Controllers
{
    public class TrafficController : BaseController
    {
        // GET: Traffic
        public async Task<ActionResult> RedirectAfterLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = await ReturnCurrentUserAsync();
                if (user.AutoPwdReplaced)
                {
                    return RedirectToAction("WeddingAdmin", "Home");
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Manage", new { replaceAutoPwd = true });
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}