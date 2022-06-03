using DbLib.DB.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib.DB
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
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; } = null!;
        public DbSet<ProductRequest> ProductRequests { get; set; } = null!;

        public PokupochkaContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PokupochkaDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductWarehouse>().HasKey(pw => new { pw.ProductId, pw.WarehouseId });
            modelBuilder.Entity<ProductWarehouse>().
                HasOne(pw => pw.Product).
                WithMany(pw => pw.ProductWarehouses).
                HasForeignKey(pw => pw.ProductId);
            modelBuilder.Entity<ProductWarehouse>().
                HasOne(pw => pw.Warehouse).
                WithMany(pw => pw.ProductWarehouses).
                HasForeignKey(pw => pw.WarehouseId);

            modelBuilder.Entity<ProductRequest>().HasKey(pr => new { pr.ProductId, pr.RequestId });
            modelBuilder.Entity<ProductRequest>().
                HasOne(pr => pr.Product).
                WithMany(pr => pr.ProductRequests).
                HasForeignKey(pr => pr.ProductId);
            modelBuilder.Entity<ProductRequest>().
                HasOne(pr => pr.Request).
                WithMany(pr => pr.ProductRequests).
                HasForeignKey(pr => pr.RequestId);
        }
    }
}
