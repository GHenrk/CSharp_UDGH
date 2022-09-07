using Microsoft.AspNetCore.Mvc;
using CSharpUdemy_MVC.Models;

namespace CSharpUdemy_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1, Name = "Eletronicos" });
            list.Add(new Department { Id = 2, Name = "Alimentício" });
            list.Add(new Department { Id = 3, Name = "Carros" });
            return View(list);
        }


    }
}
