﻿using DbLib.DB.Entity;
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
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Counterparty> Counterparties { get; set; } = null!;
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<ProductRequest> ProductRequests { get; set; } = null!;

        public PokupochkaContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PokupochkaDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductRequest>().HasKey(pr => new { pr.ProductId, pr.RequestId });
            modelBuilder.Entity<ProductRequest>().
                HasOne(pr => pr.Product).
                WithMany(pr => pr.ProductRequests).
                HasForeignKey(pr => pr.ProductId);
            modelBuilder.Entity<ProductRequest>().
                HasOne(pr => pr.Request).
                WithMany(pr => pr.ProductRequests).
                HasForeignKey(pr => pr.RequestId);

            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(15, 2);
            modelBuilder.Entity<Product>().Property(p => p.CompanyPrice).HasPrecision(15, 2);
            modelBuilder.Entity<Request>().Property(r => r.Price).HasPrecision(15, 2);
        }
    }
}
