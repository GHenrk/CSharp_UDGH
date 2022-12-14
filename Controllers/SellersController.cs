using CSharpUdemy_MVC.Models;
using CSharpUdemy_MVC.Models.ViewModels;
using CSharpUdemy_MVC.Services;
using CSharpUdemy_MVC.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public async Task<IActionResult> Index()
        {
            //Chama o método FindAll do serviço --- Serviço lista todos os Sellers;
            var list = await _SellerService.FindAllAsync();
            return View(list);
        }


        //Ação Create Seller
        public async Task<IActionResult> Create()
        {


            //Cria variável department e chama o método FindAll do DepartmentService --- Serviço lista todos os Departamentos.
            var departments = await _departmentService.FindAllAsync();
            //ViewModel um objeto enviado para complementar informações na página. Dados que serão necessários para criação do elemento.
            //Cria variável viewModel e instancia um objeto SallerFormViewModel e insere os Departamentos buscados no banco acima a lista de Departments do ViewModel;
            var viewModel = new SellerFormViewModel { Departments = departments };
            //Envia o obj viewModel para a pagina View;
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            //if (!ModelState.IsValid)
            //{
            //    var departments = await _departmentService.FindAllAsync();
            //    var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            //    return View(viewModel);
            //}
            await _SellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }


        //Delete:
        //Método Get para abrir a page de Confirmação se realmente quer excluir
        public async Task<IActionResult> Delete(int? id)
        {
            //Se id for nulo ou seja houver algo errado na requisição, responder notfound;
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Algo deu errado na requisição!" });
            }

            //Chama o método FindById do serviço seller e envia o ID (IMPORTANTE COLOCAR ID.VALUE, porque ele é um tipo anulavel)
            var obj = await _SellerService.FindByIdAsync(id.Value);

            //Se o objeto que retornar não existir, retorna notfound também;
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Esse elemento não foi encontrado!" });
            }

            //Se sucesso, chamar a View Index enviando o OBJ;
            return View(obj);
        }


        //Agora sim função POST DELETE, Chamada pelo botao do Form, enviando o ID para o SellerService;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _SellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Este vendedor possui vendas cadastradas, impossível removê-lo!" });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Algo deu errado na requisição!" });
            }


            var obj = await _SellerService.FindByIdAsync(id.Value);


            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Esse elemento não foi encontrado!" });
            }


            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //verifica se Id null
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Algo deu errado na requisição" });
            }

            //Chama sellerService e funcao find by id enviando ID;
            var obj = await _SellerService.FindByIdAsync(id.Value);

            //se retorno for igual a null
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Esse elemento não foi encontrado!" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //if (!ModelState.IsValid)
            //{
            //    var departments = await _departmentService.FindAllAsync();
            //    var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            //    return View(viewModel);
            //}
            if (id != seller.Id)
            {
                return BadRequest();

            }
            try
            {
                await _SellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }


        }



        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}
