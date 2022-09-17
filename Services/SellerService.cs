using CSharpUdemy_MVC.Data;
using CSharpUdemy_MVC.Models;
using CSharpUdemy_MVC.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CSharpUdemy_MVC.Services
{
    public class SellerService
    {

        private readonly CSharpUdemy_MVCContext _context;

        public SellerService(CSharpUdemy_MVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)

        {   //Adiciona o primeiro departamento ao objeto, para teste de cadastro!
            //obj.Department = _context.Department.First();
            _context.Seller.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            //busca no contexto o Seller, primeiro ou padrao cujo obj.Id seja igual ao Id enviado no parametro;
            //Retorna o OBJ;
            //Funcao include para carregar também o odepartment do obj;
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        { bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Elemento não encontrado");
            }
            try 
            {
                _context.Seller.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }



    }
}
