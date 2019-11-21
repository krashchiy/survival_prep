﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurvivalPrep.DBModels;

namespace SurvivalPrep.Migrations
{
    [DbContext(typeof(PrepContext))]
    [Migration("20191121022905_AddItems")]
    partial class AddItems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SurvivalPrep.DBModels.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SurvivalPrep.DBModels.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionCategoryId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionCategoryId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SurvivalPrep.DBModels.QuestionCategory", b =>
                {
                    b.Property<int>("QuestionCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScoreWeight")
                        .HasColumnType("int");

                    b.HasKey("QuestionCategoryId");

                    b.ToTable("QuestionCategories");
                });

            modelBuilder.Entity("SurvivalPrep.DBModels.Question", b =>
                {
                    b.HasOne("SurvivalPrep.DBModels.QuestionCategory", "QuestionCategory")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
