using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleContatos.Migrations
{
    /// <inheritdoc />
    public partial class CreateVinculoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Senioridade",
            //    table: "Contatos",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Contatos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contatos_UserId",
                table: "Contatos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Users_UserId",
                table: "Contatos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Users_UserId",
                table: "Contatos");

            migrationBuilder.DropIndex(
                name: "IX_Contatos_UserId",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "Senioridade",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contatos");
        }
    }
}
