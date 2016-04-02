using HoneyWedding.Services;
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

        public async Task<ActionResult> Accommodations()
        {
            var accommodationsManager = new AccommodationsManager();
            return View(await accommodationsManager.GetAsync());
        }

        public async Task<ActionResult> AccommodationDetail(int id)
        {
            var accommodationsManager = new AccommodationsManager();
            return PartialView("_AccommodationDetail", await accommodationsManager.Detail(id));
        }
    }
}
