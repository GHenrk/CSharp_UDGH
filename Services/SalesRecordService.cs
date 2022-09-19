using CSharpUdemy_MVC.Data;
using CSharpUdemy_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpUdemy_MVC.Services
{
    public class SalesRecordService
    {

        private readonly CSharpUdemy_MVCContext _context;

        public SalesRecordService(CSharpUdemy_MVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            //Has value verifica se foi informado pois pode ser null;
            if (maxDate.HasValue)
            {
                //Colocar .value sempre no objeto que pode ser nullo
                result = result.Where(x => x.Date <= maxDate.Value);

            }
            //Incluide na busca um x que leva ao x.Seller que é o objeto na tabela seller;
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();


        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            //Has value verifica se foi informado pois pode ser null;
            if (maxDate.HasValue)
            {
                //Colocar .value sempre no objeto que pode ser nullo
                result = result.Where(x => x.Date <= maxDate.Value);

            }
            //Incluide na busca um x que leva ao x.Seller que é o objeto na tabela seller;
            var data = await result.Include(x => x.Seller)
                            .Include(x => x.Seller.Department)
                            .OrderByDescending(x => x.Date)
                            .ToListAsync();

            return data.GroupBy(x => x.Seller.Department).ToList();
        }

    }
}
