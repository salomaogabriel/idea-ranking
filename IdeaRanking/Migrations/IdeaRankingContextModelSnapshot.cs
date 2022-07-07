﻿// <auto-generated />
using System;
using IdeaRanking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IdeaRanking.Migrations
{
    [DbContext(typeof(IdeaRankingContext))]
    partial class IdeaRankingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CategoryIdea", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("IdeasId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "IdeasId");

                    b.HasIndex("IdeasId");

                    b.ToTable("CategoryIdea");
                });

            modelBuilder.Entity("IdeaRanking.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("IdeaRanking.Models.Idea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BiggestRating")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfMatches")
                        .HasColumnType("int");

                    b.Property<int>("Ranking")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("IdeaRanking.Models.IdeaHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdeaId")
                        .HasColumnType("int");

                    b.Property<int>("Ranking")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("IdeaRanking.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("HasFinished")
                        .HasColumnType("bit");

                    b.Property<int?>("IdeaOneId")
                        .HasColumnType("int");

                    b.Property<int?>("IdeaTwoId")
                        .HasColumnType("int");

                    b.Property<double>("PossibleOutcomeIdeaOne")
                        .HasColumnType("float");

                    b.Property<double>("PossibleOutcomeIdeaTwo")
                        .HasColumnType("float");

                    b.Property<int>("Winner")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdeaOneId");

                    b.HasIndex("IdeaTwoId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("CategoryIdea", b =>
                {
                    b.HasOne("IdeaRanking.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdeaRanking.Models.Idea", null)
                        .WithMany()
                        .HasForeignKey("IdeasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdeaRanking.Models.IdeaHistory", b =>
                {
                    b.HasOne("IdeaRanking.Models.Idea", "Idea")
                        .WithMany("History")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Idea");
                });

            modelBuilder.Entity("IdeaRanking.Models.Match", b =>
                {
                    b.HasOne("IdeaRanking.Models.Idea", "IdeaOne")
                        .WithMany()
                        .HasForeignKey("IdeaOneId");

                    b.HasOne("IdeaRanking.Models.Idea", "IdeaTwo")
                        .WithMany()
                        .HasForeignKey("IdeaTwoId");

                    b.Navigation("IdeaOne");

                    b.Navigation("IdeaTwo");
                });

            modelBuilder.Entity("IdeaRanking.Models.Idea", b =>
                {
                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}
