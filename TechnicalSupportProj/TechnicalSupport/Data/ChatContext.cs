using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechnicalSupport.Models;

namespace TechnicalSupport.Data
{
    public class ChatContext : DbContext
    {

            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<Dialog> Dialogs { get; set; }
            public DbSet<Message> Messages { get; set; }
            public DbSet<Status> Statuses { get; set; }
            public DbSet<Employee> Employees { get; set; }





        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
     

   
        public virtual DbSet<EmployeeTask> Tasks { get; set; }
        public virtual DbSet<WorkTime> WorkTimes { get; set; }




        public ChatContext(DbContextOptions<ChatContext> options)
                : base(options)
            {
           // Database.EnsureDeleted();
          //  Database.EnsureCreated();
        }
      


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //                              .AddJsonFile("appsettings.json")
        //                              .Build();
        //        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{



        //    string adminRoleName = "admin";
        //    string userRoleName = "user";

        //    // добавляем тестовые роли
        //    Role adminRole = new Role { Id = 1, Name = adminRoleName };
        //    Role userRole = new Role { Id = 2, Name = userRoleName };

        //    //// добавляем тестовых пользователей
        //    User adminUser1 = new User { Id = Guid.NewGuid(), Email = "madmin@mail.com", Password = "123456", RoleId = adminRole.Id };
        //    User simpleUser1 = new User { Id = Guid.NewGuid(), Email = "bob@mail.com", Password = "123456", RoleId = userRole.Id };
        //    User simpleUser2 = new User { Id = Guid.NewGuid(), Email = "sam@mail.com", Password = "123456", RoleId = userRole.Id };

          
        //    Employee employee1 = new Employee { Id = Guid.NewGuid(), Email = "admin@mail.com", Password = "123456", RoleId = adminRole.Id, Name = "admin", StatusOnline = false  };
        //    Employee employee2 = new Employee { Id = Guid.NewGuid(), Email = "employee@mail.com", Password = "123456", RoleId = adminRole.Id, Name = "employee", StatusOnline = false };

        //    WorkTime workTime = new WorkTime { Id = 2, From = TimeSpan.FromMinutes(200), To = TimeSpan.FromMinutes(900), Employees = new List<Employee> { employee1, employee2 } };


        //    modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
        //    modelBuilder.Entity<User>().HasData(new User[] { adminUser1, simpleUser1, simpleUser2 });          
        //    modelBuilder.Entity<Employee>().HasData(new Employee[] { employee1, employee2 });
        //    base.OnModelCreating(modelBuilder);







            //modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            //modelBuilder.Entity<Application>(entity =>
            //{
            //    entity.ToTable("Application");

            //    entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.Topic)
            //        .IsRequired()
            //        .HasMaxLength(150);

            //    entity.HasOne(d => d.ChatNavigation)
            //        .WithMany(p => p.Applications)
            //        .HasForeignKey(d => d.Chat)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Chat");

            //    entity.HasOne(d => d.StatusNavigation)
            //        .WithMany(p => p.Applications)
            //        .HasForeignKey(d => d.Status)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Status");
            //});

          

         

            //modelBuilder.Entity<Employee>(entity =>
            //{
            //    entity.ToTable("Employee");

            //    entity.HasIndex(e => e.Id, "IX_EmployeeGUID")
            //        .IsUnique();

            //    entity.Property(e => e.Email).HasMaxLength(100);

            //    entity.Property(e => e.Id)
            //        .HasColumnName("EmployeeGUID")
            //        .HasDefaultValueSql("(newid())");

            //    entity.Property(e => e.FirstName).HasMaxLength(100);

            //    entity.Property(e => e.LastName).HasMaxLength(100);

              

            //    entity.Property(e => e.Phone).HasMaxLength(22);

            //    entity.Property(e => e.SecondName).HasMaxLength(100);


            //    entity.HasOne(d => d.WorkTimeNavigation)
            //        .WithMany(p => p.Employees)
            //        .HasForeignKey(d => d.WorkTime)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_WorkTime");
            //});

            //modelBuilder.Entity<RequestType>(entity =>
            //{
            //    entity.ToTable("RequestType");

            //    entity.Property(e => e.RequestType1)
            //        .IsRequired()
            //        .HasMaxLength(300)
            //        .HasColumnName("RequestType");
            //});

         

            //modelBuilder.Entity<Status>(entity =>
            //{
            //    entity.ToTable("Status");

            //    entity.Property(e => e.Status1)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .HasColumnName("Status");
            //});

            //modelBuilder.Entity<EmployeeTask>(entity =>
            //{
            //    entity.ToTable("Task");

            //    entity.Property(e => e.Guidemployy).HasColumnName("GUIDEmployy");

            //    entity.Property(e => e.TaskCount).HasDefaultValueSql("((1))");

            //    entity.HasOne(d => d.GuidemployyNavigation)
            //        .WithMany(p => p.Tasks)
            //        .HasPrincipalKey(p => p.Id)
            //        .HasForeignKey(d => d.Guidemployy)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Task_GUIDEmployy");
            //});

            //modelBuilder.Entity<Client>(entity =>
            //{
            //    entity.ToTable("User");

            //    //entity.HasIndex(e => e.UserGuid, "IX_UserGUID")
            //    //    .IsUnique();

            //    entity.Property(e => e.Email).HasMaxLength(100);

            //    entity.Property(e => e.FirstName).HasMaxLength(100);

            //    entity.Property(e => e.LastName).HasMaxLength(100);

            //    entity.Property(e => e.PasswordHash).HasMaxLength(64);

            //    entity.Property(e => e.Phone).HasMaxLength(22);

            //    entity.Property(e => e.SecondName).HasMaxLength(100);

            //    //entity.Property(e => e.UserGuid)
            //    //    .HasColumnName("UserGUID")
            //    //    .HasDefaultValueSql("(newid())");

            //    entity.Property(e => e.UserIp).HasMaxLength(100);

              
        //    });

         
     //   }

     



    }
}
