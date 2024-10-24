using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.Migrations
{
    /// <inheritdoc />
    public partial class addSuperCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataForInstructor_AspNetUsers_UserId",
                table: "DataForInstructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataForInstructor",
                table: "DataForInstructor");

            migrationBuilder.RenameTable(
                name: "DataForInstructor",
                newName: "DataForInstructors");

            migrationBuilder.RenameIndex(
                name: "IX_DataForInstructor_UserId",
                table: "DataForInstructors",
                newName: "IX_DataForInstructors_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CourseLevel",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SuperCategotryId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataForInstructors",
                table: "DataForInstructors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SuperCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SuperCategotryId",
                table: "Categories",
                column: "SuperCategotryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_SuperCategories_SuperCategotryId",
                table: "Categories",
                column: "SuperCategotryId",
                principalTable: "SuperCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataForInstructors_AspNetUsers_UserId",
                table: "DataForInstructors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_SuperCategories_SuperCategotryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_DataForInstructors_AspNetUsers_UserId",
                table: "DataForInstructors");

            migrationBuilder.DropTable(
                name: "SuperCategories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SuperCategotryId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataForInstructors",
                table: "DataForInstructors");

            migrationBuilder.DropColumn(
                name: "SuperCategotryId",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "DataForInstructors",
                newName: "DataForInstructor");

            migrationBuilder.RenameIndex(
                name: "IX_DataForInstructors_UserId",
                table: "DataForInstructor",
                newName: "IX_DataForInstructor_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CourseLevel",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
