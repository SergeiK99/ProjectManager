using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientCompanyName",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ExecutorCompanyName",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "ClientCompanyId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExecutorCompanyId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientCompanyId",
                table: "Project",
                column: "ClientCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ExecutorCompanyId",
                table: "Project",
                column: "ExecutorCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ClientCompany_ClientCompanyId",
                table: "Project",
                column: "ClientCompanyId",
                principalTable: "ClientCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ExecutorCompany_ExecutorCompanyId",
                table: "Project",
                column: "ExecutorCompanyId",
                principalTable: "ExecutorCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ClientCompany_ClientCompanyId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_ExecutorCompany_ExecutorCompanyId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ClientCompanyId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ExecutorCompanyId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ClientCompanyId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ExecutorCompanyId",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "ClientCompanyName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExecutorCompanyName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
