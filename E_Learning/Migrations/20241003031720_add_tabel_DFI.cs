using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.Migrations
{
    /// <inheritdoc />
    public partial class add_tabel_DFI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalUserData_AspNetUsers_UserId",
                table: "AdditionalUserData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdditionalUserData",
                table: "AdditionalUserData");

            migrationBuilder.RenameTable(
                name: "AdditionalUserData",
                newName: "DataForInstructor");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalUserData_UserId",
                table: "DataForInstructor",
                newName: "IX_DataForInstructor_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataForInstructor",
                table: "DataForInstructor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataForInstructor_AspNetUsers_UserId",
                table: "DataForInstructor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataForInstructor_AspNetUsers_UserId",
                table: "DataForInstructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataForInstructor",
                table: "DataForInstructor");

            migrationBuilder.RenameTable(
                name: "DataForInstructor",
                newName: "AdditionalUserData");

            migrationBuilder.RenameIndex(
                name: "IX_DataForInstructor_UserId",
                table: "AdditionalUserData",
                newName: "IX_AdditionalUserData_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalUserData",
                table: "AdditionalUserData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalUserData_AspNetUsers_UserId",
                table: "AdditionalUserData",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
