﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieStore.Infrastructure;

#nullable disable

namespace MovieStore.Infrastructure.Migrations
{
    [DbContext(typeof(MovieStoreContext))]
    partial class MovieStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieStore.Core.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MoneySpent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MovieStore.Core.Entities.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfRelease")
                        .HasColumnType("datetime2");

                    b.Property<int>("LicensingType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Movies", (string)null);

                    b.HasDiscriminator<int>("LicensingType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MovieStore.Core.Entities.PurchasedMovie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MovieExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MovieId");

                    b.ToTable("PurchasedMovies");
                });

            modelBuilder.Entity("MovieStore.Core.Entities.LifeLongMovie", b =>
                {
                    b.HasBaseType("MovieStore.Core.Entities.Movie");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("MovieStore.Core.Entities.TwoDayMovie", b =>
                {
                    b.HasBaseType("MovieStore.Core.Entities.Movie");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("MovieStore.Core.Entities.Customer", b =>
                {
                    b.OwnsOne("MovieStore.Core.ValueObjects.CustomerStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("StatusExpirationDateValue")
                                .HasColumnType("datetime2")
                                .HasColumnName("StatusExpirationDate");

                            b1.Property<int>("StatusValue")
                                .HasColumnType("int")
                                .HasColumnName("Status");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("MovieStore.Core.Entities.PurchasedMovie", b =>
                {
                    b.HasOne("MovieStore.Core.Entities.Customer", "Customer")
                        .WithMany("PurchasedMovies")
                        .HasForeignKey("CustomerId");

                    b.HasOne("MovieStore.Core.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.Navigation("Customer");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieStore.Core.Entities.Customer", b =>
                {
                    b.Navigation("PurchasedMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
