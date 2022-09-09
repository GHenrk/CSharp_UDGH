using CSharpUdemy_MVC.Models;
using CSharpUdemy_MVC.Models.ViewModels;
using CSharpUdemy_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpUdemy_MVC.Controllers
{
    public class SellersController : Controller
    {
        //Cria o objeto de dados como readOnly e da o nome;
        private readonly SellerService _SellerService;
        private readonly DepartmentService _departmentService;


        //Método construtor da classe ---  envia as classes como argumentos, e adiciona ao objeto criado acima;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _SellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {   
            //Chama o método FindAll do serviço --- Serviço lista todos os Sellers;
            var list = _SellerService.FindAll();
            return View(list);
        }


        //Ação Create Seller
        public IActionResult Create()
        {   
            //Cria variável department e chama o método FindAll do DepartmentService --- Serviço lista todos os Departamentos.
            var departments = _departmentService.FindAll();
            //ViewModel um objeto enviado para complementar informações na página. Dados que serão necessários para criação do elemento.
            //Cria variável viewModel e instancia um objeto SallerFormViewModel e insere os Departamentos buscados no banco acima a lista de Departments do ViewModel;
            var viewModel = new SellerFormViewModel { Departments = departments };
            //Envia o obj viewModel para a pagina View;
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _SellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
