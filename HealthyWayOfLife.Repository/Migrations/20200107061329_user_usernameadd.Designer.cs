﻿// <auto-generated />
using System;
using HealthyWayOfLife.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HealthyWayOfLife.Repository.Migrations
{
    [DbContext(typeof(HealthyWayOfLifeDbContext))]
    [Migration("20200107061329_user_usernameadd")]
    partial class user_usernameadd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HealthyWayOfLife.Model.Models.Database.Biometry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("ArmInCm")
                        .HasColumnType("decimal(19,4)");

                    b.Property<decimal>("ChestInCm")
                        .HasColumnType("decimal(19,4)");

                    b.Property<DateTime>("DateFor");

                    b.Property<decimal>("HeightInCm")
                        .HasColumnType("decimal(19,4)");

                    b.Property<int>("InsertBy");

                    b.Property<DateTime>("InsertDate");

                    b.Property<decimal>("LegInCm")
                        .HasColumnType("decimal(19,4)");

                    b.Property<int>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<int?>("UserId");

                    b.Property<decimal>("WaistInCm")
                        .HasColumnType("decimal(19,4)");

                    b.Property<decimal>("WeightInKgs")
                        .HasColumnType("decimal(19,4)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Biometry");
                });

            modelBuilder.Entity("HealthyWayOfLife.Model.Models.Database.Configuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("BoolValue");

                    b.Property<int>("ConfigType");

                    b.Property<string>("ConfigTypeName");

                    b.Property<DateTime?>("DateTimeValue");

                    b.Property<decimal?>("DecimalValue")
                        .HasColumnType("decimal(19,4)");

                    b.Property<int>("InsertBy");

                    b.Property<DateTime>("InsertDate");

                    b.Property<int?>("IntValue");

                    b.Property<int?>("OrderNumber");

                    b.Property<string>("StringValue");

                    b.Property<int>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<int>("ValueType");

                    b.HasKey("Id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("HealthyWayOfLife.Model.Models.Database.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Exception");

                    b.Property<string>("InnerException");

                    b.Property<int>("InsertBy");

                    b.Property<DateTime>("InsertDate");

                    b.Property<string>("LogText");

                    b.Property<int>("LogType");

                    b.Property<string>("Stack");

                    b.Property<int>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("HealthyWayOfLife.Model.Models.Database.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndTime");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<int>("InsertBy");

                    b.Property<DateTime>("InsertDate");

                    b.Property<DateTime?>("LastRefreshDate");

                    b.Property<string>("RemoteAddress")
                        .IsRequired();

                    b.Property<int>("SessionState");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("Token")
                        .IsRequired();

                    b.Property<int>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("HealthyWayOfLife.Model.Models.Database.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("InsertBy");

                    b.Property<DateTime>("InsertDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Login");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("UpdateBy");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HealthyWayOfLife.Model.Models.Database.Biometry", b =>
                {
                    b.HasOne("HealthyWayOfLife.Model.Models.Database.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HealthyWayOfLife.Model.Models.Database.Session", b =>
                {
                    b.HasOne("HealthyWayOfLife.Model.Models.Database.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}