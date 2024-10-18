using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolutisHelpDesk.Migrations
{
    /// <inheritdoc />
    public partial class Deletecascadeclientechamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Clientes_ClienteId",
                table: "Chamados");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Clientes_ClienteId",
                table: "Chamados",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Clientes_ClienteId",
                table: "Chamados");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Clientes_ClienteId",
                table: "Chamados",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId");
        }
    }
}
