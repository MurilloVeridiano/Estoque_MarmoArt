using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarmorariaProjeto.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarMovimentacaoEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosEntrada");

            migrationBuilder.DropTable(
                name: "HistoricosSaida");

            migrationBuilder.CreateTable(
                name: "MovimentacaoEstoque",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemEstoqueId = table.Column<long>(type: "bigint", nullable: false),
                    ItensEstoqueId = table.Column<long>(type: "bigint", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuncionarioPegou = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuncionarioEntregou = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacaoEstoque_ItensEstoque_ItensEstoqueId",
                        column: x => x.ItensEstoqueId,
                        principalTable: "ItensEstoque",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoEstoque_ItensEstoqueId",
                table: "MovimentacaoEstoque",
                column: "ItensEstoqueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentacaoEstoque");

            migrationBuilder.CreateTable(
                name: "HistoricosEntrada",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItensEstoqueId = table.Column<long>(type: "bigint", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemEstoqueId = table.Column<long>(type: "bigint", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
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
                    ItensEstoqueId = table.Column<long>(type: "bigint", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Funcionario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemEstoqueId = table.Column<long>(type: "bigint", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
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
    }
}
