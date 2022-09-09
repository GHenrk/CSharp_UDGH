using CSharpUdemy_MVC.Data;
using CSharpUdemy_MVC.Models;

namespace CSharpUdemy_MVC.Services
{
    public class SellerService
    {

        private readonly CSharpUdemy_MVCContext _context;

        public SellerService(CSharpUdemy_MVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)

        {   //Adiciona o primeiro departamento ao objeto, para teste de cadastro!
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            //busca no contexto o Seller, primeiro ou padrao cujo obj.Id seja igual ao Id enviado no parametro;
            //Retorna o OBJ;
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }



    }
}
