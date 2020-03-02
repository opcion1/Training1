﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Training1.Models;

namespace Training1.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20200213145513_SesshinMenu")]
    partial class SesshinMenu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Training1.Models.DayOfSesshin", b =>
                {
                    b.Property<int>("DayOfSesshinId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("NumberOfPeople");

                    b.Property<int>("SesshinId");

                    b.HasKey("DayOfSesshinId");

                    b.HasIndex("SesshinId");

                    b.ToTable("DayOfSesshin");
                });

            modelBuilder.Entity("Training1.Models.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Commentary");

                    b.Property<string>("Description");

                    b.Property<int?>("MealId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("FoodId");

                    b.HasIndex("MealId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("Training1.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FoodId");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("UnityType");

                    b.HasKey("IngredientId");

                    b.HasIndex("FoodId");

                    b.HasIndex("ProductId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("Training1.Models.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfSesshinId");

                    b.Property<int>("Type");

                    b.HasKey("MealId");

                    b.HasIndex("DayOfSesshinId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("Training1.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Training1.Models.Sesshin", b =>
                {
                    b.Property<int>("SesshinId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("NumberOfPeople");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("SesshinId");

                    b.ToTable("Sesshin");
                });

            modelBuilder.Entity("Training1.Models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CommandDate");

                    b.Property<int>("Currency");

                    b.Property<decimal?>("PricePorUnity")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProductId");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("UnityType");

                    b.HasKey("StockId");

                    b.HasIndex("ProductId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Training1.Models.DayOfSesshin", b =>
                {
                    b.HasOne("Training1.Models.Sesshin", "Sesshin")
                        .WithMany("Days")
                        .HasForeignKey("SesshinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Training1.Models.Food", b =>
                {
                    b.HasOne("Training1.Models.Meal")
                        .WithMany("Foods")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("Training1.Models.Ingredient", b =>
                {
                    b.HasOne("Training1.Models.Food", "Food")
                        .WithMany("Ingredients")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Training1.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Training1.Models.Meal", b =>
                {
                    b.HasOne("Training1.Models.DayOfSesshin", "DayOfSesshin")
                        .WithMany("Meals")
                        .HasForeignKey("DayOfSesshinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Training1.Models.Stock", b =>
                {
                    b.HasOne("Training1.Models.Product", "Product")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
