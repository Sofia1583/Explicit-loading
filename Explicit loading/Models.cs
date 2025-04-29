using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Explicit_loading
{
    internal class Models
    {
        public class ApplicationContext : DbContext
        {
            public DbSet<User> Users { get; set; } = null!;
            public DbSet<Company> Companies { get; set; } = null!;

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=helloapp.db");
            }
        }
        public class Company
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public List<User> Users { get; set; } = new();
        }
        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }

            public int? CompanyId { get; set; }
            public Company? Company { get; set; }
        }
    }
}
