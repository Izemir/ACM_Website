﻿// <auto-generated />
using System;
using ACM_API.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ACM_API.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220607090356_initial34")]
    partial class initial34
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ACM_API.Models.Chat.Chat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ExecutorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ExecutorId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("ACM_API.Models.Chat.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SenderName")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("ACM_API.Models.Contact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<long?>("ContactPersonId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ContactTypeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ExecutorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ContactPersonId");

                    b.HasIndex("ContactTypeId");

                    b.HasIndex("ExecutorId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("ACM_API.Models.ContactType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("TypeName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ContactType");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.Construction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConstructionName")
                        .HasColumnType("text");

                    b.Property<long?>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Constructions");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.ContactPerson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("ContactPerson");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ActualAddress")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<long?>("CustomerTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("INN")
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("KPP")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OGRN")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerTypeId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.CustomerType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("TypeName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CustomerTypes");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.Industry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("IndustryName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Industry");
                });

            modelBuilder.Entity("ACM_API.Models.Executor.Competency", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CompetencyName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Competencies");
                });

            modelBuilder.Entity("ACM_API.Models.Executor.Executor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Approved")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("INN")
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Executors");
                });

            modelBuilder.Entity("ACM_API.Models.Executor.Speciality", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("SpecialityName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("ACM_API.Models.Service", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ServiceName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ACM_API.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<long?>("ExecutorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("ExecutorId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ACM_API.Models.UserFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long?>("ExecutorId")
                        .HasColumnType("bigint");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExecutorId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("CompetencyExecutor", b =>
                {
                    b.Property<long>("CompetencyId")
                        .HasColumnType("bigint");

                    b.Property<long>("ExecutorId")
                        .HasColumnType("bigint");

                    b.HasKey("CompetencyId", "ExecutorId");

                    b.HasIndex("ExecutorId");

                    b.ToTable("CompetencyExecutor");
                });

            modelBuilder.Entity("CompetencyService", b =>
                {
                    b.Property<long>("CompetencyId")
                        .HasColumnType("bigint");

                    b.Property<long>("ServiceId")
                        .HasColumnType("bigint");

                    b.HasKey("CompetencyId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("CompetencyService");
                });

            modelBuilder.Entity("ConstructionService", b =>
                {
                    b.Property<long>("ConstructionsId")
                        .HasColumnType("bigint");

                    b.Property<long>("ServicesId")
                        .HasColumnType("bigint");

                    b.HasKey("ConstructionsId", "ServicesId");

                    b.HasIndex("ServicesId");

                    b.ToTable("ConstructionService");
                });

            modelBuilder.Entity("ExecutorSpeciality", b =>
                {
                    b.Property<long>("ExecutorId")
                        .HasColumnType("bigint");

                    b.Property<long>("SpecialityId")
                        .HasColumnType("bigint");

                    b.HasKey("ExecutorId", "SpecialityId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("ExecutorSpeciality");
                });

            modelBuilder.Entity("ACM_API.Models.Chat.Chat", b =>
                {
                    b.HasOne("ACM_API.Models.Customer.Customer", "Customer")
                        .WithMany("Chats")
                        .HasForeignKey("CustomerId");

                    b.HasOne("ACM_API.Models.Executor.Executor", "Executor")
                        .WithMany("Chats")
                        .HasForeignKey("ExecutorId");

                    b.Navigation("Customer");

                    b.Navigation("Executor");
                });

            modelBuilder.Entity("ACM_API.Models.Chat.Message", b =>
                {
                    b.HasOne("ACM_API.Models.Chat.Chat", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("ACM_API.Models.Contact", b =>
                {
                    b.HasOne("ACM_API.Models.Customer.ContactPerson", null)
                        .WithMany("Contacts")
                        .HasForeignKey("ContactPersonId");

                    b.HasOne("ACM_API.Models.ContactType", "ContactType")
                        .WithMany()
                        .HasForeignKey("ContactTypeId");

                    b.HasOne("ACM_API.Models.Executor.Executor", null)
                        .WithMany("Contacts")
                        .HasForeignKey("ExecutorId");

                    b.Navigation("ContactType");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.Construction", b =>
                {
                    b.HasOne("ACM_API.Models.Customer.Customer", null)
                        .WithMany("Constructions")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.ContactPerson", b =>
                {
                    b.HasOne("ACM_API.Models.Customer.Customer", null)
                        .WithMany("ContactPersons")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.Customer", b =>
                {
                    b.HasOne("ACM_API.Models.Customer.CustomerType", "CustomerType")
                        .WithMany()
                        .HasForeignKey("CustomerTypeId");

                    b.Navigation("CustomerType");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.Industry", b =>
                {
                    b.HasOne("ACM_API.Models.Customer.Customer", null)
                        .WithMany("Industries")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("ACM_API.Models.User", b =>
                {
                    b.HasOne("ACM_API.Models.Customer.Customer", "Customer")
                        .WithOne("User")
                        .HasForeignKey("ACM_API.Models.User", "CustomerId");

                    b.HasOne("ACM_API.Models.Executor.Executor", "Executor")
                        .WithOne("User")
                        .HasForeignKey("ACM_API.Models.User", "ExecutorId");

                    b.Navigation("Customer");

                    b.Navigation("Executor");
                });

            modelBuilder.Entity("ACM_API.Models.UserFile", b =>
                {
                    b.HasOne("ACM_API.Models.Executor.Executor", null)
                        .WithMany("Files")
                        .HasForeignKey("ExecutorId");
                });

            modelBuilder.Entity("CompetencyExecutor", b =>
                {
                    b.HasOne("ACM_API.Models.Executor.Competency", null)
                        .WithMany()
                        .HasForeignKey("CompetencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ACM_API.Models.Executor.Executor", null)
                        .WithMany()
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompetencyService", b =>
                {
                    b.HasOne("ACM_API.Models.Executor.Competency", null)
                        .WithMany()
                        .HasForeignKey("CompetencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ACM_API.Models.Service", null)
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionService", b =>
                {
                    b.HasOne("ACM_API.Models.Customer.Construction", null)
                        .WithMany()
                        .HasForeignKey("ConstructionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ACM_API.Models.Service", null)
                        .WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExecutorSpeciality", b =>
                {
                    b.HasOne("ACM_API.Models.Executor.Executor", null)
                        .WithMany()
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ACM_API.Models.Executor.Speciality", null)
                        .WithMany()
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ACM_API.Models.Chat.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.ContactPerson", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("ACM_API.Models.Customer.Customer", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Constructions");

                    b.Navigation("ContactPersons");

                    b.Navigation("Industries");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ACM_API.Models.Executor.Executor", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Contacts");

                    b.Navigation("Files");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
