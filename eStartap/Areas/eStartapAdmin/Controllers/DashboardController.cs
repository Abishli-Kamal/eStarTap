using Microsoft.AspNetCore.Mvc;

namespace eStartap.Areas.eStartapAdmin.Controllers
{
    [Area("eStartapAdmin")]
    public class DashboardController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
