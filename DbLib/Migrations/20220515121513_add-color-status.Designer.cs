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
    [Migration("20220515121513_add-color-status")]
    partial class addcolorstatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContractProduct", b =>
                {
                    b.Property<int>("ContractsId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("ContractsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ContractProduct");
                });

            modelBuilder.Entity("DbLib.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CountYears")
                        .HasColumnType("int");

                    b.Property<int?>("CounterpartyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOver")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CounterpartyId");

                    b.HasIndex("StatusId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("DbLib.Counterparty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Counterparties");
                });

            modelBuilder.Entity("DbLib.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DbLib.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DbImageId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("DbLib.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");
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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("ContractProduct", b =>
                {
                    b.HasOne("DbLib.Contract", null)
                        .WithMany()
                        .HasForeignKey("ContractsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbLib.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DbLib.Contract", b =>
                {
                    b.HasOne("DbLib.Counterparty", "Counterparty")
                        .WithMany("Contracts")
                        .HasForeignKey("CounterpartyId");

                    b.HasOne("DbLib.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("DbLib.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId");

                    b.Navigation("Counterparty");

                    b.Navigation("Status");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("DbLib.Counterparty", b =>
                {
                    b.HasOne("DbLib.User", "User")
                        .WithOne("Counterparty")
                        .HasForeignKey("DbLib.Counterparty", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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
                        .HasForeignKey("DbLib.Worker", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DbLib.Counterparty", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("DbLib.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DbLib.User", b =>
                {
                    b.Navigation("Counterparty");

                    b.Navigation("Worker");
                });
#pragma warning restore 612, 618
        }
    }
}
