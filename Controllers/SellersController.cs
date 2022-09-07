using Microsoft.AspNetCore.Mvc;

namespace CSharpUdemy_MVC.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
