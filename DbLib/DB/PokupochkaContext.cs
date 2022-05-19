using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class PokupochkaContext : DbContext
    {
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Worker> Workers { get; set; } = null!;
        public DbSet<Counterparty> Counterparties { get; set; } = null!;
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        

        public PokupochkaContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PokupochkaDB;Trusted_Connection=True;");
        }
    }
}
