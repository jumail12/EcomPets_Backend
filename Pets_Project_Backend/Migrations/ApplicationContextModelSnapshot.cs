﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pets_Project_Backend.Context;

#nullable disable

namespace Pets_Project_Backend.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.AddressModel.UserAddress", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("AddressId");

                    b.HasIndex("userId");

                    b.ToTable("UserAddress");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.CartModel.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.CartModel.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemId"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductQty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.CategoryModel.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.OrderModel.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("OderPlaced");

                    b.Property<string>("OrderString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("AddressId");

                    b.HasIndex("userId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.OrderModel.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.ProductModel.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OfferPrize")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(50);

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.UserModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("User");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool?>("isBlocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.WhishListModel.WhishList", b =>
                {
                    b.Property<int>("whishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("whishId"));

                    b.Property<int?>("productId")
                        .HasColumnType("int");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("whishId");

                    b.HasIndex("productId");

                    b.HasIndex("userId");

                    b.ToTable("WhishList");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.AddressModel.UserAddress", b =>
                {
                    b.HasOne("Pets_Project_Backend.Data.Models.UserModels.User", "_userAd")
                        .WithMany("_UserAddresses")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_userAd");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.CartModel.Cart", b =>
                {
                    b.HasOne("Pets_Project_Backend.Data.Models.UserModels.User", "_User")
                        .WithOne("_Cart")
                        .HasForeignKey("Pets_Project_Backend.Data.Models.CartModel.Cart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_User");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.CartModel.CartItem", b =>
                {
                    b.HasOne("Pets_Project_Backend.Data.Models.CartModel.Cart", "_Cart")
                        .WithMany("_Items")
                        .HasForeignKey("CartId");

                    b.HasOne("Pets_Project_Backend.Data.Models.ProductModel.Product", "_Product")
                        .WithMany("_CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("_Cart");

                    b.Navigation("_Product");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.OrderModel.Order", b =>
                {
                    b.HasOne("Pets_Project_Backend.Data.Models.AddressModel.UserAddress", "_UserAd")
                        .WithMany("_orders")
                        .HasForeignKey("AddressId");

                    b.HasOne("Pets_Project_Backend.Data.Models.UserModels.User", "_user")
                        .WithMany("_Orders")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_UserAd");

                    b.Navigation("_user");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.OrderModel.OrderItem", b =>
                {
                    b.HasOne("Pets_Project_Backend.Data.Models.OrderModel.Order", "_order")
                        .WithMany("_Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pets_Project_Backend.Data.Models.ProductModel.Product", "_product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_order");

                    b.Navigation("_product");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.ProductModel.Product", b =>
                {
                    b.HasOne("Pets_Project_Backend.Data.Models.CategoryModel.Category", "_Category")
                        .WithMany("_Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("_Category");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.WhishListModel.WhishList", b =>
                {
                    b.HasOne("Pets_Project_Backend.Data.Models.ProductModel.Product", "_Product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pets_Project_Backend.Data.Models.UserModels.User", "_User")
                        .WithMany("_WhishLists")
                        .HasForeignKey("userId");

                    b.Navigation("_Product");

                    b.Navigation("_User");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.AddressModel.UserAddress", b =>
                {
                    b.Navigation("_orders");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.CartModel.Cart", b =>
                {
                    b.Navigation("_Items");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.CategoryModel.Category", b =>
                {
                    b.Navigation("_Products");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.OrderModel.Order", b =>
                {
                    b.Navigation("_Items");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.ProductModel.Product", b =>
                {
                    b.Navigation("_CartItems");
                });

            modelBuilder.Entity("Pets_Project_Backend.Data.Models.UserModels.User", b =>
                {
                    b.Navigation("_Cart");

                    b.Navigation("_Orders");

                    b.Navigation("_UserAddresses");

                    b.Navigation("_WhishLists");
                });
#pragma warning restore 612, 618
        }
    }
}
