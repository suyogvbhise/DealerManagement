﻿// <auto-generated />
using System;
using DMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250302113155_AddPDN1")]
    partial class AddPDN1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DMS.Models.FuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");
                });

            modelBuilder.Entity("DMS.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FuelTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("PurchaseLocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleOwnership")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FuelTypeId");

                    b.HasIndex("PurchaseLocationId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("DMS.Models.PurchaseDeliveryNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuyerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("PurchaseDeliveryNotes");
                });

            modelBuilder.Entity("DMS.Models.PurchaseLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PurchaseLocations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Showroom A"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Dealer X"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Warehouse Y"
                        });
                });

            modelBuilder.Entity("DMS.Models.Inventory", b =>
                {
                    b.HasOne("DMS.Models.FuelType", "FuelType")
                        .WithMany("Inventories")
                        .HasForeignKey("FuelTypeId");

                    b.HasOne("DMS.Models.PurchaseLocation", "PurchaseLocations")
                        .WithMany("Inventories")
                        .HasForeignKey("PurchaseLocationId");

                    b.Navigation("FuelType");

                    b.Navigation("PurchaseLocations");
                });

            modelBuilder.Entity("DMS.Models.PurchaseDeliveryNote", b =>
                {
                    b.HasOne("DMS.Models.Inventory", "Inventory")
                        .WithMany("PurchaseDeliveryNotes")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("DMS.Models.FuelType", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("DMS.Models.Inventory", b =>
                {
                    b.Navigation("PurchaseDeliveryNotes");
                });

            modelBuilder.Entity("DMS.Models.PurchaseLocation", b =>
                {
                    b.Navigation("Inventories");
                });
#pragma warning restore 612, 618
        }
    }
}
