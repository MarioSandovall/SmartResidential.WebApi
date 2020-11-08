﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(ResidentialContext))]
    [Migration("20201011164348_RelationshipAddedBetweenHousesAndUsers")]
    partial class RelationshipAddedBetweenHousesAndUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.ActionEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ResidentialId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ResidentialId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("Domain.Entities.AdvertisementEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ResidentialId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ResidentialId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("Domain.Entities.AnnouncementEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("ResidentialId")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ResidentialId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("Domain.Entities.AreaEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("ResidentialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ResidentialId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Domain.Entities.AttachmentEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ActionId")
                        .HasColumnType("int");

                    b.Property<int?>("AnnouncementId")
                        .HasColumnType("int");

                    b.Property<int>("AttachmentType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int?>("ReportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("AnnouncementId");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("ReportId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Domain.Entities.AttachmentTypeEf", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("AttachmentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Document"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Image"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ExpenseCategoryEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ResidentialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResidentialId");

                    b.ToTable("ExpenseCategories");
                });

            modelBuilder.Entity("Domain.Entities.ExpenseEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("ExpenseCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ExpenseCategoryId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Domain.Entities.HouseEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InteriorNumber")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("OutdoorNumber")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("ResidentialId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("ResidentialId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("Domain.Entities.HouseUserEf", b =>
                {
                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("HouseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("HouseUsers");
                });

            modelBuilder.Entity("Domain.Entities.PaymentCategoryEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("ResidentialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResidentialId");

                    b.ToTable("PaymentCategories");
                });

            modelBuilder.Entity("Domain.Entities.PaymentEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HouseId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.HasIndex("PaymentCategoryId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Entities.PaymentStatusEf", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PaymentStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Paid out"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Domain.Entities.PaymentTypeEf", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cash"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Credit Card"
                        },
                        new
                        {
                            Id = 3,
                            Name = "cheque"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Payment transfer"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ReportEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("ReportStatus")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Domain.Entities.ReportMessageEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReportId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.HasIndex("UserId");

                    b.ToTable("ReportMessages");
                });

            modelBuilder.Entity("Domain.Entities.ReportStatusEf", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ReportStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Opened"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cancelled"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ReservationEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ApproverId")
                        .HasColumnType("int");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequesterId")
                        .HasColumnType("int");

                    b.Property<int>("ReservationStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("AreaId");

                    b.HasIndex("RequesterId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Domain.Entities.ReservationStatusEf", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ReservationStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Not Approved"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ResidentialEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Cellphone")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("LandPhone")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("LogoPath")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ResidentialStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResidentialStatusId");

                    b.ToTable("Residentials");
                });

            modelBuilder.Entity("Domain.Entities.ResidentialStatusEf", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ResidentialStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Inactive"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Outstanding"
                        });
                });

            modelBuilder.Entity("Domain.Entities.RoleEf", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SuperAdmin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Resident"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Vigilant"
                        });
                });

            modelBuilder.Entity("Domain.Entities.UserEf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CellPhone")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LandPhone")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ProfileImagePath")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<int?>("ResidentialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResidentialId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.UserRoleEf", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.ActionEf", b =>
                {
                    b.HasOne("Domain.Entities.ResidentialEf", "Residential")
                        .WithMany()
                        .HasForeignKey("ResidentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.AdvertisementEf", b =>
                {
                    b.HasOne("Domain.Entities.ResidentialEf", "Residential")
                        .WithMany()
                        .HasForeignKey("ResidentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.AnnouncementEf", b =>
                {
                    b.HasOne("Domain.Entities.UserEf", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ResidentialEf", "Residential")
                        .WithMany()
                        .HasForeignKey("ResidentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.AreaEf", b =>
                {
                    b.HasOne("Domain.Entities.UserEf", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ResidentialEf", "Residential")
                        .WithMany()
                        .HasForeignKey("ResidentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.AttachmentEf", b =>
                {
                    b.HasOne("Domain.Entities.ActionEf", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId");

                    b.HasOne("Domain.Entities.AnnouncementEf", "Announcement")
                        .WithMany()
                        .HasForeignKey("AnnouncementId");

                    b.HasOne("Domain.Entities.ExpenseEf", "Expense")
                        .WithMany()
                        .HasForeignKey("ExpenseId");

                    b.HasOne("Domain.Entities.PaymentEf", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("Domain.Entities.ReportEf", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId");
                });

            modelBuilder.Entity("Domain.Entities.ExpenseCategoryEf", b =>
                {
                    b.HasOne("Domain.Entities.ResidentialEf", "Residential")
                        .WithMany()
                        .HasForeignKey("ResidentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ExpenseEf", b =>
                {
                    b.HasOne("Domain.Entities.UserEf", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ExpenseCategoryEf", "ExpenseCategory")
                        .WithMany()
                        .HasForeignKey("ExpenseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.HouseEf", b =>
                {
                    b.HasOne("Domain.Entities.ResidentialEf", "Residential")
                        .WithMany()
                        .HasForeignKey("ResidentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.HouseUserEf", b =>
                {
                    b.HasOne("Domain.Entities.HouseEf", "House")
                        .WithMany("HouseUsers")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserEf", "User")
                        .WithMany("HouseUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.PaymentCategoryEf", b =>
                {
                    b.HasOne("Domain.Entities.ResidentialEf", "Residential")
                        .WithMany()
                        .HasForeignKey("ResidentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.PaymentEf", b =>
                {
                    b.HasOne("Domain.Entities.HouseEf", "House")
                        .WithMany()
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PaymentCategoryEf", "PaymentCategory")
                        .WithMany()
                        .HasForeignKey("PaymentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ReportEf", b =>
                {
                    b.HasOne("Domain.Entities.UserEf", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ReportMessageEf", b =>
                {
                    b.HasOne("Domain.Entities.ReportEf", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserEf", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ReservationEf", b =>
                {
                    b.HasOne("Domain.Entities.UserEf", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverId");

                    b.HasOne("Domain.Entities.AreaEf", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserEf", "Requester")
                        .WithMany()
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ResidentialEf", b =>
                {
                    b.HasOne("Domain.Entities.ResidentialStatusEf", "ResidentialStatus")
                        .WithMany()
                        .HasForeignKey("ResidentialStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.UserEf", b =>
                {
                    b.HasOne("Domain.Entities.ResidentialEf", "Residential")
                        .WithMany()
                        .HasForeignKey("ResidentialId");
                });

            modelBuilder.Entity("Domain.Entities.UserRoleEf", b =>
                {
                    b.HasOne("Domain.Entities.RoleEf", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.UserEf", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
