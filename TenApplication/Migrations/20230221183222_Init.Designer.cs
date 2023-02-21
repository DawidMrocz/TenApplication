﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TenApplication.Data;

#nullable disable

namespace TenApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230221183222_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TenApplication.Models.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("TenApplication.Models.Cat", b =>
                {
                    b.Property<Guid>("CatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CatDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CatId");

                    b.HasIndex("UserId");

                    b.ToTable("Cats");
                });

            modelBuilder.Entity("TenApplication.Models.CatModels.CatRecordCell", b =>
                {
                    b.Property<Guid>("CatRecordCellId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CatRecordCellDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CatRecordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("CellHours")
                        .HasPrecision(2)
                        .HasColumnType("float(2)");

                    b.HasKey("CatRecordCellId");

                    b.HasIndex("CatRecordId");

                    b.ToTable("CatRecordCells");
                });

            modelBuilder.Entity("TenApplication.Models.CatRecord", b =>
                {
                    b.Property<Guid>("CatRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InboxItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CatRecordId");

                    b.HasIndex("CatId");

                    b.HasIndex("InboxItemId")
                        .IsUnique()
                        .HasFilter("[InboxItemId] IS NOT NULL");

                    b.ToTable("CatRecords");
                });

            modelBuilder.Entity("TenApplication.Models.Inbox", b =>
                {
                    b.Property<Guid>("InboxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InboxId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Inboxs");
                });

            modelBuilder.Entity("TenApplication.Models.InboxItem", b =>
                {
                    b.Property<Guid>("InboxItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Components")
                        .HasColumnType("int");

                    b.Property<int>("DrawingsAssembly")
                        .HasColumnType("int");

                    b.Property<int>("DrawingsComponents")
                        .HasColumnType("int");

                    b.Property<double>("Hours")
                        .HasPrecision(2)
                        .HasColumnType("float(2)");

                    b.Property<Guid?>("InboxId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.HasKey("InboxItemId");

                    b.HasIndex("InboxId");

                    b.HasIndex("JobId");

                    b.ToTable("InboxItems");
                });

            modelBuilder.Entity("TenApplication.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<int?>("Client")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Ecm")
                        .HasColumnType("int");

                    b.Property<string>("Engineer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Finished")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gpdm")
                        .HasColumnType("int");

                    b.Property<string>("JobDescription")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Received")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Software")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Started")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TaskType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            JobId = 1,
                            Ecm = 32016495,
                            Gpdm = 0,
                            JobDescription = "Create muffler",
                            Region = "NA",
                            Software = "Catia",
                            Status = 0,
                            TaskType = "Models"
                        },
                        new
                        {
                            JobId = 2,
                            Ecm = 32016408,
                            Gpdm = 0,
                            JobDescription = "Create pipe",
                            Region = "NA",
                            Software = "NX",
                            Status = 10,
                            TaskType = "Models"
                        },
                        new
                        {
                            JobId = 3,
                            Ecm = 32016497,
                            Gpdm = 0,
                            JobDescription = "Create drawing",
                            Region = "NA",
                            Software = "Catia",
                            Status = 20,
                            TaskType = "Drawings"
                        },
                        new
                        {
                            JobId = 4,
                            Ecm = 32016485,
                            Gpdm = 0,
                            JobDescription = "Update drawing",
                            Region = "NA",
                            Software = "Catia",
                            Status = 0,
                            TaskType = "Drawings"
                        },
                        new
                        {
                            JobId = 5,
                            Ecm = 32016464,
                            Gpdm = 0,
                            JobDescription = "Partition holes",
                            Region = "CN",
                            Software = "Catia",
                            Status = 100,
                            TaskType = "Models"
                        },
                        new
                        {
                            JobId = 6,
                            Ecm = 32016435,
                            Gpdm = 0,
                            JobDescription = "Hot end proposal",
                            Region = "NA",
                            Software = "Catia",
                            Status = 45,
                            TaskType = "Both"
                        });
                });

            modelBuilder.Entity("TenApplication.Models.RaportModels.Raport", b =>
                {
                    b.Property<Guid>("RaportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RaportDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RaportId");

                    b.ToTable("Raports");
                });

            modelBuilder.Entity("TenApplication.Models.RaportModels.RaportRecord", b =>
                {
                    b.Property<Guid>("RaportRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InboxItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RaportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RaportRecordDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("RaportRecordHours")
                        .HasPrecision(2)
                        .HasColumnType("float(2)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RaportRecordId");

                    b.HasIndex("InboxItemId");

                    b.HasIndex("RaportId");

                    b.HasIndex("UserId");

                    b.ToTable("RaportRecords");
                });

            modelBuilder.Entity("TenApplication.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ActTyp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CCtr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TennecoStartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("TenApplication.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("TenApplication.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("TenApplication.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("TenApplication.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TenApplication.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("TenApplication.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TenApplication.Models.Cat", b =>
                {
                    b.HasOne("TenApplication.Models.User", "User")
                        .WithMany("Cats")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TenApplication.Models.CatModels.CatRecordCell", b =>
                {
                    b.HasOne("TenApplication.Models.CatRecord", "CatRecord")
                        .WithMany("CatRecordCells")
                        .HasForeignKey("CatRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatRecord");
                });

            modelBuilder.Entity("TenApplication.Models.CatRecord", b =>
                {
                    b.HasOne("TenApplication.Models.Cat", "Cat")
                        .WithMany("CatRecords")
                        .HasForeignKey("CatId");

                    b.HasOne("TenApplication.Models.InboxItem", "InboxItem")
                        .WithOne("CatRecord")
                        .HasForeignKey("TenApplication.Models.CatRecord", "InboxItemId");

                    b.Navigation("Cat");

                    b.Navigation("InboxItem");
                });

            modelBuilder.Entity("TenApplication.Models.Inbox", b =>
                {
                    b.HasOne("TenApplication.Models.User", "User")
                        .WithOne("Inbox")
                        .HasForeignKey("TenApplication.Models.Inbox", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TenApplication.Models.InboxItem", b =>
                {
                    b.HasOne("TenApplication.Models.Inbox", "Inbox")
                        .WithMany("InboxItems")
                        .HasForeignKey("InboxId");

                    b.HasOne("TenApplication.Models.Job", "Job")
                        .WithMany("InboxItems")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inbox");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("TenApplication.Models.RaportModels.RaportRecord", b =>
                {
                    b.HasOne("TenApplication.Models.InboxItem", "InboxItem")
                        .WithMany()
                        .HasForeignKey("InboxItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TenApplication.Models.RaportModels.Raport", null)
                        .WithMany("RaportRecords")
                        .HasForeignKey("RaportId");

                    b.HasOne("TenApplication.Models.User", null)
                        .WithMany("RaportRecordss")
                        .HasForeignKey("UserId");

                    b.Navigation("InboxItem");
                });

            modelBuilder.Entity("TenApplication.Models.Cat", b =>
                {
                    b.Navigation("CatRecords");
                });

            modelBuilder.Entity("TenApplication.Models.CatRecord", b =>
                {
                    b.Navigation("CatRecordCells");
                });

            modelBuilder.Entity("TenApplication.Models.Inbox", b =>
                {
                    b.Navigation("InboxItems");
                });

            modelBuilder.Entity("TenApplication.Models.InboxItem", b =>
                {
                    b.Navigation("CatRecord");
                });

            modelBuilder.Entity("TenApplication.Models.Job", b =>
                {
                    b.Navigation("InboxItems");
                });

            modelBuilder.Entity("TenApplication.Models.RaportModels.Raport", b =>
                {
                    b.Navigation("RaportRecords");
                });

            modelBuilder.Entity("TenApplication.Models.User", b =>
                {
                    b.Navigation("Cats");

                    b.Navigation("Inbox")
                        .IsRequired();

                    b.Navigation("RaportRecordss");
                });
#pragma warning restore 612, 618
        }
    }
}
