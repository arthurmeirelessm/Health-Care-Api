using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthCareApi.Migrations
{
    public partial class changedToPatientproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteForMedicalCares_Users_UserId",
                table: "NoteForMedicalCares");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "NoteForMedicalCares",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteForMedicalCares_UserId",
                table: "NoteForMedicalCares",
                newName: "IX_NoteForMedicalCares_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteForMedicalCares_Users_PatientId",
                table: "NoteForMedicalCares",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteForMedicalCares_Users_PatientId",
                table: "NoteForMedicalCares");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "NoteForMedicalCares",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NoteForMedicalCares_PatientId",
                table: "NoteForMedicalCares",
                newName: "IX_NoteForMedicalCares_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteForMedicalCares_Users_UserId",
                table: "NoteForMedicalCares",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
