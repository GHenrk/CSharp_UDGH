using CSharpUdemy_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CSharpUdemy_MVC.Models.ViewModels;
namespace CSharpUdemy_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Teste"] = "Edicao view Data teste";
            ViewData["Teste1"] = "Edicao view Data teste";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}