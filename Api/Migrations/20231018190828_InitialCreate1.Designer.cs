﻿// <auto-generated />
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(SchoolDBContext))]
    [Migration("20231018190828_InitialCreate1")]
    partial class InitialCreate1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Answer", b =>
                {
                    b.Property<int>("AnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerText")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("AnswerID");

                    b.HasIndex("StudentID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("AssessmentAnswer", b =>
                {
                    b.Property<int>("AssessmentAnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssessmentAreaID")
                        .HasColumnType("int");

                    b.Property<int>("CorrectAnswerID")
                        .HasColumnType("int");

                    b.HasKey("AssessmentAnswerID");

                    b.HasIndex("AssessmentAreaID");

                    b.HasIndex("CorrectAnswerID");

                    b.ToTable("AssessmentAnswers");
                });

            modelBuilder.Entity("AssessmentArea", b =>
                {
                    b.Property<int>("AreaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AreaName")
                        .HasColumnType("longtext");

                    b.HasKey("AreaID");

                    b.ToTable("AssessmentAreas");
                });

            modelBuilder.Entity("AssessmentSummary", b =>
                {
                    b.Property<int>("AssessmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssessmentAnswerID")
                        .HasColumnType("int");

                    b.Property<int>("AssessmentAreaID")
                        .HasColumnType("int");

                    b.Property<decimal>("AverageScore")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("AwardID")
                        .HasColumnType("int");

                    b.Property<decimal>("CorrectAnswerPercentagePerClass")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("CreditCount")
                        .HasColumnType("int");

                    b.Property<int>("DistinctCount")
                        .HasColumnType("int");

                    b.Property<int>("HighDistinctCount")
                        .HasColumnType("int");

                    b.Property<int>("ParticipantCount")
                        .HasColumnType("int");

                    b.Property<string>("ParticipantStatus")
                        .HasColumnType("longtext");

                    b.Property<int>("SchoolPercentile")
                        .HasColumnType("int");

                    b.Property<string>("StrengthStatus")
                        .HasColumnType("longtext");

                    b.Property<decimal>("StudentAreaAssessedScore")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<decimal>("StudentScore")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("StudentTotalAssessed")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("SubjectClassID")
                        .HasColumnType("int");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.Property<int>("SydneyPercentile")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAreaAssessedScore")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("AssessmentID");

                    b.HasIndex("AssessmentAnswerID");

                    b.HasIndex("AssessmentAreaID");

                    b.HasIndex("AwardID");

                    b.HasIndex("StudentID");

                    b.HasIndex("SubjectClassID");

                    b.HasIndex("SubjectID");

                    b.ToTable("AssessmentSummaries");
                });

            modelBuilder.Entity("Award", b =>
                {
                    b.Property<int>("AwardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AwardName")
                        .HasColumnType("longtext");

                    b.HasKey("AwardID");

                    b.ToTable("Awards");
                });

            modelBuilder.Entity("Class", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName")
                        .HasColumnType("longtext");

                    b.Property<int>("SchoolID")
                        .HasColumnType("int");

                    b.HasKey("ClassID");

                    b.HasIndex("SchoolID");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("CorrectAnswer", b =>
                {
                    b.Property<int>("CorrectAnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CorrectAnswerText")
                        .HasColumnType("longtext");

                    b.HasKey("CorrectAnswerID");

                    b.ToTable("CorrectAnswers");
                });

            modelBuilder.Entity("School", b =>
                {
                    b.Property<int>("SchoolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SchoolName")
                        .HasColumnType("longtext");

                    b.HasKey("SchoolID");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<int>("YearLevel")
                        .HasColumnType("int");

                    b.HasKey("StudentID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Subject", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SubjectName")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SubjectScore")
                        .HasColumnType("int");

                    b.HasKey("SubjectID");

                    b.HasIndex("SubjectName")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SubjectClass", b =>
                {
                    b.Property<int>("SubjectClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.HasKey("SubjectClassID");

                    b.HasIndex("ClassID");

                    b.HasIndex("SubjectID");

                    b.ToTable("SubjectClasses");
                });

            modelBuilder.Entity("Answer", b =>
                {
                    b.HasOne("Student", "Student")
                        .WithMany("Answers")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AssessmentAnswer", b =>
                {
                    b.HasOne("AssessmentArea", "AssessmentArea")
                        .WithMany()
                        .HasForeignKey("AssessmentAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CorrectAnswer", "CorrectAnswer")
                        .WithMany()
                        .HasForeignKey("CorrectAnswerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssessmentArea");

                    b.Navigation("CorrectAnswer");
                });

            modelBuilder.Entity("AssessmentSummary", b =>
                {
                    b.HasOne("AssessmentAnswer", "AssessmentAnswer")
                        .WithMany()
                        .HasForeignKey("AssessmentAnswerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssessmentArea", "AssessmentArea")
                        .WithMany()
                        .HasForeignKey("AssessmentAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Award", "Award")
                        .WithMany("AssessmentSummaries")
                        .HasForeignKey("AwardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student", "Student")
                        .WithMany("AssessmentSummaries")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SubjectClass", "SubjectClass")
                        .WithMany()
                        .HasForeignKey("SubjectClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssessmentAnswer");

                    b.Navigation("AssessmentArea");

                    b.Navigation("Award");

                    b.Navigation("Student");

                    b.Navigation("Subject");

                    b.Navigation("SubjectClass");
                });

            modelBuilder.Entity("Class", b =>
                {
                    b.HasOne("School", "School")
                        .WithMany("Classes")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("SubjectClass", b =>
                {
                    b.HasOne("Class", "Class")
                        .WithMany("SubjectClasses")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subject", "Subject")
                        .WithMany("SubjectClasses")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Award", b =>
                {
                    b.Navigation("AssessmentSummaries");
                });

            modelBuilder.Entity("Class", b =>
                {
                    b.Navigation("SubjectClasses");
                });

            modelBuilder.Entity("School", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("AssessmentSummaries");
                });

            modelBuilder.Entity("Subject", b =>
                {
                    b.Navigation("SubjectClasses");
                });
#pragma warning restore 612, 618
        }
    }
}