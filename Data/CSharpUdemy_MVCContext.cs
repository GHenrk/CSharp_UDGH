using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSharpUdemy_MVC.Models;

namespace CSharpUdemy_MVC.Data
{
    public class CSharpUdemy_MVCContext : DbContext
    {
        public CSharpUdemy_MVCContext(DbContextOptions<CSharpUdemy_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = default!;

        public DbSet<Seller> Seller { get; set; } = default!;

        public DbSet<SalesRecord> SalesRecord { get; set; } = default!;
    }
}
