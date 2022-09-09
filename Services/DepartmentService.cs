using CSharpUdemy_MVC.Data;
using CSharpUdemy_MVC.Models;

namespace CSharpUdemy_MVC.Services
{
    public class DepartmentService
    {

        private readonly CSharpUdemy_MVCContext _context;

        public DepartmentService(CSharpUdemy_MVCContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
