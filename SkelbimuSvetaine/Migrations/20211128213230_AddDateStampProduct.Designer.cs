﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkelbimuSvetaine.Models;

namespace SkelbimuSvetaine.Migrations
{
    [DbContext(typeof(ld1_gynimasContext))]
    [Migration("20211128213230_AddDateStampProduct")]
    partial class AddDateStampProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("SkelbimuSvetaine.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int(11)")
                        .HasColumnName("Product_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int(11)")
                        .HasColumnName("User_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProductId" }, "fk_Comment_Product1_idx");

                    b.HasIndex(new[] { "UserId" }, "fk_Comment_User1_idx");

                    b.ToTable("comment");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int(11)")
                        .HasColumnName("Category_id");

                    b.Property<DateTime>("CreatedTimestamp")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("description");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("blob")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("double(10)")
                        .HasColumnName("price");

                    b.Property<int>("UserId")
                        .HasColumnType("int(11)")
                        .HasColumnName("User_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CategoryId" }, "fk_Product_Category1_idx");

                    b.HasIndex(new[] { "UserId" }, "fk_Product_User_idx");

                    b.ToTable("product");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<int>("ProductId")
                        .HasColumnType("int(11)")
                        .HasColumnName("Product_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int(11)")
                        .HasColumnName("User_id");

                    b.Property<int>("Value")
                        .HasColumnType("int(11)")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProductId" }, "fk_Rating_Product1_idx");

                    b.HasIndex(new[] { "UserId" }, "fk_Rating_User1_idx");

                    b.ToTable("rating");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<byte[]>("Icon")
                        .HasColumnType("blob")
                        .HasColumnName("icon")
                        .HasDefaultValueSql("'NULL'");

                    b.Property<string>("Miestas")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("miestas");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("phone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.Comment", b =>
                {
                    b.HasOne("SkelbimuSvetaine.Models.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fk_Comment_Product1")
                        .IsRequired();

                    b.HasOne("SkelbimuSvetaine.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_Comment_User1")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.Product", b =>
                {
                    b.HasOne("SkelbimuSvetaine.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("fk_Product_Category1")
                        .IsRequired();

                    b.HasOne("SkelbimuSvetaine.Models.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_Product_User")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.Rating", b =>
                {
                    b.HasOne("SkelbimuSvetaine.Models.Product", "Product")
                        .WithMany("Ratings")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fk_Rating_Product1")
                        .IsRequired();

                    b.HasOne("SkelbimuSvetaine.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_Rating_User1")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.Product", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("SkelbimuSvetaine.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Products");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
