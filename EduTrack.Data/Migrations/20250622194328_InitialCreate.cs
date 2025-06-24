using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassLessons",
                columns: table => new
                {
                    ClassRoomId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLessons", x => new { x.ClassRoomId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_ClassLessons_Classes_ClassRoomId",
                        column: x => x.ClassRoomId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassUsers",
                columns: table => new
                {
                    ClassRoomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassUsers", x => new { x.ClassRoomId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ClassUsers_Classes_ClassRoomId",
                        column: x => x.ClassRoomId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherClassLessons",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    ClassRoomId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    ClassLessonClassRoomId = table.Column<int>(type: "int", nullable: true),
                    ClassLessonLessonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherClassLessons", x => new { x.TeacherId, x.ClassRoomId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_TeacherClassLessons_ClassLessons_ClassLessonClassRoomId_ClassLessonLessonId",
                        columns: x => new { x.ClassLessonClassRoomId, x.ClassLessonLessonId },
                        principalTable: "ClassLessons",
                        principalColumns: new[] { "ClassRoomId", "LessonId" });
                    table.ForeignKey(
                        name: "FK_TeacherClassLessons_Classes_ClassRoomId",
                        column: x => x.ClassRoomId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherClassLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherClassLessons_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassUserClassId = table.Column<int>(type: "int", nullable: false),
                    ClassUserUserId = table.Column<int>(type: "int", nullable: false),
                    ClassUserClassRoomId = table.Column<int>(type: "int", nullable: false),
                    ClassLessonClassId = table.Column<int>(type: "int", nullable: false),
                    ClassLessonLessonId = table.Column<int>(type: "int", nullable: false),
                    ClassLessonClassRoomId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_ClassLessons_ClassLessonClassRoomId_ClassLessonLessonId",
                        columns: x => new { x.ClassLessonClassRoomId, x.ClassLessonLessonId },
                        principalTable: "ClassLessons",
                        principalColumns: new[] { "ClassRoomId", "LessonId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_ClassUsers_ClassUserClassRoomId_ClassUserUserId",
                        columns: x => new { x.ClassUserClassRoomId, x.ClassUserUserId },
                        principalTable: "ClassUsers",
                        principalColumns: new[] { "ClassRoomId", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SchoolId",
                table: "Classes",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLessons_LessonId",
                table: "ClassLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassUsers_UserId",
                table: "ClassUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ClassLessonClassRoomId_ClassLessonLessonId",
                table: "Notes",
                columns: new[] { "ClassLessonClassRoomId", "ClassLessonLessonId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ClassUserClassRoomId_ClassUserUserId",
                table: "Notes",
                columns: new[] { "ClassUserClassRoomId", "ClassUserUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClassLessons_ClassLessonClassRoomId_ClassLessonLessonId",
                table: "TeacherClassLessons",
                columns: new[] { "ClassLessonClassRoomId", "ClassLessonLessonId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClassLessons_ClassRoomId",
                table: "TeacherClassLessons",
                column: "ClassRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClassLessons_LessonId",
                table: "TeacherClassLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolId",
                table: "Users",
                column: "SchoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "TeacherClassLessons");

            migrationBuilder.DropTable(
                name: "ClassUsers");

            migrationBuilder.DropTable(
                name: "ClassLessons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
