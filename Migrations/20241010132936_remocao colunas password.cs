using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolutisHelpDesk.Migrations
{
    /// <inheritdoc />
    public partial class remocaocolunaspassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmation",
                table: "Tecnicos");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Tecnicos");

            migrationBuilder.DropColumn(
                name: "RePassword",
                table: "Tecnicos");

            migrationBuilder.DropColumn(
                name: "EmailConfirmation",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "RePassword",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EmailConfirmation",
                table: "Administradores");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Administradores");

            migrationBuilder.DropColumn(
                name: "RePassword",
                table: "Administradores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailConfirmation",
                table: "Tecnicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Tecnicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RePassword",
                table: "Tecnicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailConfirmation",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RePassword",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailConfirmation",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RePassword",
                table: "Administradores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
