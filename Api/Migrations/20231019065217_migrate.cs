using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentSummaries");

            migrationBuilder.DropTable(
                name: "AssessmentAnswers");

            migrationBuilder.DropTable(
                name: "CorrectAnswers");

            migrationBuilder.AddColumn<int>(
                name: "AssessmentAreaID",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswerText",
                table: "Answers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SchoolClasses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClasses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SchoolClasses_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolClasses_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentAnswers",
                columns: table => new
                {
                    StudentAnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    AnswerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswers", x => x.StudentAnswerID);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Answers_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "Answers",
                        principalColumn: "AnswerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentClasses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ClassroomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClasses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Classes_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AssessmentAreaID",
                table: "Answers",
                column: "AssessmentAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_ClassID",
                table: "SchoolClasses",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_SchoolID",
                table: "SchoolClasses",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_AnswerID",
                table: "StudentAnswers",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_StudentID",
                table: "StudentAnswers",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_ClassroomID",
                table: "StudentClasses",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_StudentID",
                table: "StudentClasses",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AssessmentAreas_AssessmentAreaID",
                table: "Answers",
                column: "AssessmentAreaID",
                principalTable: "AssessmentAreas",
                principalColumn: "AreaID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AssessmentAreas_AssessmentAreaID",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "SchoolClasses");

            migrationBuilder.DropTable(
                name: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "StudentClasses");

            migrationBuilder.DropIndex(
                name: "IX_Answers_AssessmentAreaID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "AssessmentAreaID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerText",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "CorrectAnswers",
                columns: table => new
                {
                    CorrectAnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CorrectAnswerText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectAnswers", x => x.CorrectAnswerID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentAnswers",
                columns: table => new
                {
                    AssessmentAnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentAreaID = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentAnswers", x => x.AssessmentAnswerID);
                    table.ForeignKey(
                        name: "FK_AssessmentAnswers_AssessmentAreas_AssessmentAreaID",
                        column: x => x.AssessmentAreaID,
                        principalTable: "AssessmentAreas",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentAnswers_CorrectAnswers_CorrectAnswerID",
                        column: x => x.CorrectAnswerID,
                        principalTable: "CorrectAnswers",
                        principalColumn: "CorrectAnswerID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssessmentSummaries",
                columns: table => new
                {
                    AssessmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentAnswerID = table.Column<int>(type: "int", nullable: false),
                    AssessmentAreaID = table.Column<int>(type: "int", nullable: false),
                    AwardID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    SubjectClassID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    AverageScore = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CorrectAnswerPercentagePerClass = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreditCount = table.Column<int>(type: "int", nullable: false),
                    DistinctCount = table.Column<int>(type: "int", nullable: false),
                    HighDistinctCount = table.Column<int>(type: "int", nullable: false),
                    ParticipantCount = table.Column<int>(type: "int", nullable: false),
                    ParticipantStatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolPercentile = table.Column<int>(type: "int", nullable: false),
                    StrengthStatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentAreaAssessedScore = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    StudentScore = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    StudentTotalAssessed = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SydneyPercentile = table.Column<int>(type: "int", nullable: false),
                    TotalAreaAssessedScore = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSummaries", x => x.AssessmentID);
                    table.ForeignKey(
                        name: "FK_AssessmentSummaries_AssessmentAnswers_AssessmentAnswerID",
                        column: x => x.AssessmentAnswerID,
                        principalTable: "AssessmentAnswers",
                        principalColumn: "AssessmentAnswerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSummaries_AssessmentAreas_AssessmentAreaID",
                        column: x => x.AssessmentAreaID,
                        principalTable: "AssessmentAreas",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSummaries_Awards_AwardID",
                        column: x => x.AwardID,
                        principalTable: "Awards",
                        principalColumn: "AwardID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSummaries_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSummaries_SubjectClasses_SubjectClassID",
                        column: x => x.SubjectClassID,
                        principalTable: "SubjectClasses",
                        principalColumn: "SubjectClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSummaries_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswers_AssessmentAreaID",
                table: "AssessmentAnswers",
                column: "AssessmentAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswers_CorrectAnswerID",
                table: "AssessmentAnswers",
                column: "CorrectAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSummaries_AssessmentAnswerID",
                table: "AssessmentSummaries",
                column: "AssessmentAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSummaries_AssessmentAreaID",
                table: "AssessmentSummaries",
                column: "AssessmentAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSummaries_AwardID",
                table: "AssessmentSummaries",
                column: "AwardID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSummaries_StudentID",
                table: "AssessmentSummaries",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSummaries_SubjectClassID",
                table: "AssessmentSummaries",
                column: "SubjectClassID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSummaries_SubjectID",
                table: "AssessmentSummaries",
                column: "SubjectID");
        }
    }
}
