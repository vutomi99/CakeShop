﻿// <auto-generated />
using CakeShop.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CakeShop.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241207111350_addProductsToDB")]
    partial class addProductsToDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CakeShop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Cars"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Home Deco"
                        });
                });

            modelBuilder.Entity("CakeShop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("supplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Powerful and reliable for any Engine",
                            ListPrice = 90.0,
                            Name = "Njk Spark Plugs",
                            ProductId = 254,
                            supplier = "GoldWagen"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Powerful 1975 Piano",
                            ListPrice = 47000.0,
                            Name = "Grand Piano",
                            ProductId = 4,
                            supplier = "Takealot"
                        },
                        new
                        {
                            Id = 3,
                            Description = "12cm,14cm office table with chair",
                            ListPrice = 90.0,
                            Name = "Office Table",
                            ProductId = 54,
                            supplier = "Hassan and Hassan"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
