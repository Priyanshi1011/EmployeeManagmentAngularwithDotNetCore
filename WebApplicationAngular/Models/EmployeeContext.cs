using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebApplicationAngular.Models;
namespace WebApplicationAngular.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
        public DbSet<EmployeeModel> Employees { get; set; }
        public IConfigurationRoot Configuration { get; private set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;port=3306;database=employeedb;user=root;password=Promact2019");
        }
        public DbSet<WebApplicationAngular.Models.DepartmentModel> Department { get; set; }


    }
}
