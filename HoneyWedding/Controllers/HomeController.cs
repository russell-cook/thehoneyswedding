using HoneyWedding.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HoneyWedding.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Accommodations()
        {
            return View();
        }

        public async Task<ActionResult> AccommodationDetail(int id)
        {
            if (id == 0) // special case for Accommodations page load
            {
                return PartialView("_AccommodationWelcome");
            }
            var accommodationsManager = new AccommodationsManager();
            return PartialView("_AccommodationDetail", await accommodationsManager.Detail(id));
        }

        public async Task<ActionResult> AccommodationsList(string sortOrder)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DistanceSortParm = sortOrder == "Distance" ? "distance_desc" : "Distance";
            ViewBag.BallerSortParm = sortOrder == "Baller" ? "baller_desc" : "Baller";
            ViewBag.SleepsSortParm = sortOrder == "Sleeps" ? "sleeps_desc" : "Sleeps";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var accommodationsManager = new AccommodationsManager();
            return PartialView("_AccommodationsList", await accommodationsManager.GetAsync(sortOrder));
        }

        public async Task<ActionResult> AccommodationsAccordion(string sortOrder)
        {
            //ViewBag.SortOrder = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DistanceSortParm = sortOrder == "Distance" ? "distance_desc" : "Distance";
            //ViewBag.BallerSortParm = sortOrder == "Baller" ? "baller_desc" : "Baller";
            //ViewBag.SleepsSortParm = sortOrder == "Sleeps" ? "sleeps_desc" : "Sleeps";
            //ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var accommodationsManager = new AccommodationsManager();
            return PartialView("_AccommodationsAccordion", await accommodationsManager.GetAsync(""));
        }

        [Authorize(Roles = "WeddingAdmin")]
        public ActionResult WeddingAdmin()
        {
            return View();
        }
    }
}
