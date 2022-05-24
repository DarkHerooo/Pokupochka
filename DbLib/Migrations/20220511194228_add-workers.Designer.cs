﻿// <auto-generated />
using System;
using DbLib.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbLib.Migrations
{
    [DbContext(typeof(PokupochkaContext))]
    [Migration("20220511194228_add-workers")]
    partial class addworkers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DbLib.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("DbLib.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("DbLib.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOver")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("DbLib.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DbLib.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DbLib.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("DbLib.Client", b =>
                {
                    b.HasOne("DbLib.Company", "Company")
                        .WithMany("Clients")
                        .HasForeignKey("CompanyId");

                    b.HasOne("DbLib.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("DbLib.Client", "UserId");

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DbLib.Contract", b =>
                {
                    b.HasOne("DbLib.Client", "Client")
                        .WithMany("Contracts")
                        .HasForeignKey("ClientId");

                    b.HasOne("DbLib.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId");

                    b.Navigation("Client");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("DbLib.User", b =>
                {
                    b.HasOne("DbLib.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DbLib.Worker", b =>
                {
                    b.HasOne("DbLib.User", "User")
                        .WithOne("Worker")
                        .HasForeignKey("DbLib.Worker", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DbLib.Client", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("DbLib.Company", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("DbLib.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DbLib.User", b =>
                {
                    b.Navigation("Client");

                    b.Navigation("Worker");
                });
#pragma warning restore 612, 618
        }
    }
}
