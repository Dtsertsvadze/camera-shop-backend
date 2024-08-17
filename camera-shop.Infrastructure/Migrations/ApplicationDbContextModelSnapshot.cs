﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using camera_shop.Infrastructure.Data;

#nullable disable

namespace camera_shop.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("camera_shop.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Camera Equipment"
                        },
                        new
                        {
                            Id = 2,
                            ParentCategoryId = 1,
                            Title = "Cameras"
                        },
                        new
                        {
                            Id = 3,
                            ParentCategoryId = 1,
                            Title = "Lenses"
                        },
                        new
                        {
                            Id = 4,
                            ParentCategoryId = 1,
                            Title = "Accessories"
                        },
                        new
                        {
                            Id = 5,
                            ParentCategoryId = 2,
                            Title = "DSLR"
                        },
                        new
                        {
                            Id = 6,
                            ParentCategoryId = 2,
                            Title = "Mirrorless"
                        },
                        new
                        {
                            Id = 7,
                            ParentCategoryId = 2,
                            Title = "Point-and-Shoot"
                        },
                        new
                        {
                            Id = 8,
                            ParentCategoryId = 3,
                            Title = "Prime Lenses"
                        },
                        new
                        {
                            Id = 9,
                            ParentCategoryId = 3,
                            Title = "Zoom Lenses"
                        },
                        new
                        {
                            Id = 10,
                            ParentCategoryId = 3,
                            Title = "Wide-Angle Lenses"
                        },
                        new
                        {
                            Id = 11,
                            ParentCategoryId = 4,
                            Title = "Camera Bags"
                        },
                        new
                        {
                            Id = 12,
                            ParentCategoryId = 4,
                            Title = "Tripods"
                        },
                        new
                        {
                            Id = 13,
                            ParentCategoryId = 4,
                            Title = "Memory Cards"
                        });
                });

            modelBuilder.Entity("camera_shop.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("BrandId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrls")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("camera_shop.Core.Entities.ProductBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Canon"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Nikon"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Sony"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Fujifilm"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Panasonic"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Olympus"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Leica"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Pentax"
                        });
                });

            modelBuilder.Entity("camera_shop.Core.Entities.Category", b =>
                {
                    b.HasOne("camera_shop.Core.Entities.Category", "ParentCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("camera_shop.Core.Entities.Product", b =>
                {
                    b.HasOne("camera_shop.Core.Entities.ProductBrand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("camera_shop.Core.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("camera_shop.Core.Entities.Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("camera_shop.Core.Entities.ProductBrand", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
