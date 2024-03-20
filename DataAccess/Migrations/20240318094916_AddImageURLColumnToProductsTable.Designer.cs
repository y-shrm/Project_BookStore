﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_BookStore.DataAccess.Data;

#nullable disable

namespace Project_BookStore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240318094916_AddImageURLColumnToProductsTable")]
    partial class AddImageURLColumnToProductsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project_BookStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Sci-Fi"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("Project_BookStore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("nvarchar(90)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Allen Kim Lang",
                            CategoryId = 1,
                            Description = "World in a Bottle is a captivating science fiction novella authored by using Allen Kim Lang.\r\n The story immerses readers in a world of clinical marvel and moral dilemmas.\r\nSet in a futuristic society the narrative follows Dr. Martin Hale a notable scientist with a imaginative and prescient\r\n to create a self-contained miniature universe inside a tumbler bottle.",
                            ImageURL = "",
                            Price = 99.0,
                            Title = "World in a Bottle"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Richard A. Proctor",
                            CategoryId = 2,
                            Description = "Myths and Marvels of Astronomy is a fascinating paintings authored with the aid of Richard A.\r\n Proctor a prominent 19th-century British astronomer and writer. This ebook takes readers on an enlightening journey thru the fascinating world \r\nof astronomy debunking myths even as revealing the awe-inspiring marvels of the universe. ",
                            ImageURL = "",
                            Price = 299.0,
                            Title = "Myths And Marvels Of\r\nAstronomy"
                        });
                });

            modelBuilder.Entity("Project_BookStore.Models.Product", b =>
                {
                    b.HasOne("Project_BookStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
