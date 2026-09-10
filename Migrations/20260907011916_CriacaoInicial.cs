using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarmorariaProjeto.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItensEstoque",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadeAtual = table.Column<int>(type: "int", nullable: false),
                    Fornecedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstoqueMinimo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensEstoque", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosEntrada",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemEstoqueId = table.Column<long>(type: "bigint", nullable: false),
                    ItensEstoqueId = table.Column<long>(type: "bigint", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosEntrada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosEntrada_ItensEstoque_ItensEstoqueId",
                        column: x => x.ItensEstoqueId,
                        principalTable: "ItensEstoque",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoricosSaida",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemEstoqueId = table.Column<long>(type: "bigint", nullable: false),
                    ItensEstoqueId = table.Column<long>(type: "bigint", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Funcionario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosSaida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosSaida_ItensEstoque_ItensEstoqueId",
                        column: x => x.ItensEstoqueId,
                        principalTable: "ItensEstoque",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosEntrada_ItensEstoqueId",
                table: "HistoricosEntrada",
                column: "ItensEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosSaida_ItensEstoqueId",
                table: "HistoricosSaida",
                column: "ItensEstoqueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosEntrada");

            migrationBuilder.DropTable(
                name: "HistoricosSaida");

            migrationBuilder.DropTable(
                name: "ItensEstoque");
        }
    }
}
