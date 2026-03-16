using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Nacionalidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioUltimaAtualizacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ISBN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DataPublicacao = table.Column<DateOnly>(type: "date", nullable: false),
                    Genero = table.Column<string>(type: "text", nullable: true),
                    Preco = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    AutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioUltimaAtualizacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livros_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autores_Nome_DataNascimento_Nacionalidade",
                table: "Autores",
                columns: new[] { "Nome", "DataNascimento", "Nacionalidade" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AutorId",
                table: "Livros",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_ISBN",
                table: "Livros",
                column: "ISBN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
