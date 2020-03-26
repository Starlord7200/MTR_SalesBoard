﻿// <auto-generated />
using System;
using MTRSalesBoard.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MTRSalesBoard.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20200326164026_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MTRSalesBoard.Models.AppUser", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MTRSalesBoard.Models.Sale", b =>
                {
                    b.Property<int>("SaleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("SaleAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("nameUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("saleDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SaleID");

                    b.HasIndex("nameUserID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("MTRSalesBoard.Models.Sale", b =>
                {
                    b.HasOne("MTRSalesBoard.Models.AppUser", "name")
                        .WithMany("Sales")
                        .HasForeignKey("nameUserID");
                });
#pragma warning restore 612, 618
        }
    }
}
