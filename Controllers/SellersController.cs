using CSharpUdemy_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpUdemy_MVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _SellerService;

        public SellersController(SellerService sellerService)
        {
            _SellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _SellerService.FindAll();
            return View(list);
        }

    }
}
