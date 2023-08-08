using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_departments_DepartmentID",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studentSubjects",
                table: "studentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_studentSubjects_StudID",
                table: "studentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departmetSubjects",
                table: "departmetSubjects");

            migrationBuilder.DropIndex(
                name: "IX_departmetSubjects_DID",
                table: "departmetSubjects");

            migrationBuilder.DropColumn(
                name: "StudSubID",
                table: "studentSubjects");

            migrationBuilder.DropColumn(
                name: "DeptSubID",
                table: "departmetSubjects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "subjects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<decimal>(
                name: "Grade",
                table: "studentSubjects",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "InsManager",
                table: "departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_studentSubjects",
                table: "studentSubjects",
                columns: new[] { "StudID", "SubID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_departmetSubjects",
                table: "departmetSubjects",
                columns: new[] { "DID", "SubID" });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InsId);
                    table.ForeignKey(
                        name: "FK_Instructor_Instructor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Instructor_departments_DID",
                        column: x => x.DID,
                        principalTable: "departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorSubject",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSubject", x => new { x.SubId, x.InsId });
                    table.ForeignKey(
                        name: "FK_InstructorSubject_Instructor_InsId",
                        column: x => x.InsId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSubject_subjects_SubId",
                        column: x => x.SubId,
                        principalTable: "subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_InsManager",
                table: "departments",
                column: "InsManager",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_DID",
                table: "Instructor",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_SupervisorId",
                table: "Instructor",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubject_InsId",
                table: "InstructorSubject",
                column: "InsId");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_Instructor_InsManager",
                table: "departments",
                column: "InsManager",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_students_departments_DepartmentID",
                table: "students",
                column: "DepartmentID",
                principalTable: "departments",
                principalColumn: "DID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_Instructor_InsManager",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_students_departments_DepartmentID",
                table: "students");

            migrationBuilder.DropTable(
                name: "InstructorSubject");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studentSubjects",
                table: "studentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departmetSubjects",
                table: "departmetSubjects");

            migrationBuilder.DropIndex(
                name: "IX_departments_InsManager",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "studentSubjects");

            migrationBuilder.DropColumn(
                name: "InsManager",
                table: "departments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Period",
                table: "subjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudSubID",
                table: "studentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeptSubID",
                table: "departmetSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_studentSubjects",
                table: "studentSubjects",
                column: "StudSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departmetSubjects",
                table: "departmetSubjects",
                column: "DeptSubID");

            migrationBuilder.CreateIndex(
                name: "IX_studentSubjects_StudID",
                table: "studentSubjects",
                column: "StudID");

            migrationBuilder.CreateIndex(
                name: "IX_departmetSubjects_DID",
                table: "departmetSubjects",
                column: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_students_departments_DepartmentID",
                table: "students",
                column: "DepartmentID",
                principalTable: "departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
