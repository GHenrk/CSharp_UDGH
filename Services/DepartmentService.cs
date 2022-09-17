using CSharpUdemy_MVC.Data;
using CSharpUdemy_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpUdemy_MVC.Services
{
    public class DepartmentService
    {

        private readonly CSharpUdemy_MVCContext _context;

        public DepartmentService(CSharpUdemy_MVCContext context)
        {
            _context = context;
        }


        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
