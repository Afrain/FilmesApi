using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesApi.Migrations
{
    public partial class RelacionandoCinemaEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Cinema",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cinema_EnderecoId",
                table: "Cinema",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinema_Enderecos_EnderecoId",
                table: "Cinema",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinema_Enderecos_EnderecoId",
                table: "Cinema");

            migrationBuilder.DropIndex(
                name: "IX_Cinema_EnderecoId",
                table: "Cinema");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Cinema");
        }
    }
}
