using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalSupport.Models;

namespace TechnicalSupport.Data
{
    public class UserContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {

            User user = new User { UserId = 1, Email = "mail@mail", RoleId = 1 };
            User employee = new User { UserId = 2, Phone = "213", RoleId = 2 };

            Employee e = new Employee { Id = 1, Phone = "213" };
            Client c = new Client { Id = 1, Email = "mail@mail" };

            builder.Entity<User>().HasData(user, employee);
            builder.Entity<Employee>().HasData(e);
            builder.Entity<Client>().HasData(c);

            base.OnModelCreating(builder);
        }
    }
}
