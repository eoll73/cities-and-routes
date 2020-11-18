﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(CityRouteContext))]
    partial class CityRouteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("MapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RouteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("DataAccess.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("image");

                    b.Property<DateTimeOffset>("UpdatedOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("DataAccess.Models.Map", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("UpdatedOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Map");
                });

            modelBuilder.Entity("DataAccess.Models.Route", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.Property<Guid>("FirstCityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FirstCityId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SecondCityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SecondCityId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("FirstCityId1");

                    b.HasIndex("MapId");

                    b.HasIndex("SecondCityId1");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("DataAccess.Models.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("DisplayingGraph")
                        .HasColumnType("bit");

                    b.Property<bool>("DisplayingImage")
                        .HasColumnType("bit");

                    b.Property<string>("EdgeColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("EdgeSize")
                        .HasColumnType("float");

                    b.Property<Guid>("MapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MapId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedOnUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("VertexColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("VertexSize")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.HasIndex("MapId1")
                        .IsUnique()
                        .HasFilter("[MapId1] IS NOT NULL");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("DataAccess.Models.City", b =>
                {
                    b.HasOne("DataAccess.Models.Map", "Map")
                        .WithMany("Cities")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Models.Map", b =>
                {
                    b.HasOne("DataAccess.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Models.Route", b =>
                {
                    b.HasOne("DataAccess.Models.City", "FirstCity")
                        .WithMany()
                        .HasForeignKey("FirstCityId1");

                    b.HasOne("DataAccess.Models.Map", "Map")
                        .WithMany("Routes")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.City", "SecondCity")
                        .WithMany()
                        .HasForeignKey("SecondCityId1");
                });

            modelBuilder.Entity("DataAccess.Models.Settings", b =>
                {
                    b.HasOne("DataAccess.Models.Map", "Map")
                        .WithMany()
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Map", null)
                        .WithOne("Settings")
                        .HasForeignKey("DataAccess.Models.Settings", "MapId1");
                });
#pragma warning restore 612, 618
        }
    }
}
