using ACM_API.Models;
using ACM_API.Models.Chat;
using ACM_API.Models.Customer;
using ACM_API.Models.Executor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.DB
{
    public class DBContext: DbContext
    {
        public DbSet <Customer> Customers { get; set; }
        public DbSet<Executor> Executors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }

        public DbSet<Competency> Competencies { get; set; }

        public DbSet<Speciality> Specialities { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Construction> Constructions { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<UserFile> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.110.105;Port=5432;Database=postgres;Username=su;Password=138200");
        }



    }


}
