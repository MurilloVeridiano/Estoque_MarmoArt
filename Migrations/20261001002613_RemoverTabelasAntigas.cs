using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarmorariaProjeto.Migrations
{
    /// <inheritdoc />
    public partial class RemoverTabelasAntigas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacaoEstoque_ItensEstoque_ItensEstoqueId",
                table: "MovimentacaoEstoque");

            migrationBuilder.DropIndex(
                name: "IX_MovimentacaoEstoque_ItensEstoqueId",
                table: "MovimentacaoEstoque");

            migrationBuilder.DropColumn(
                name: "ItensEstoqueId",
                table: "MovimentacaoEstoque");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "MovimentacaoEstoque",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.Sql("""
                UPDATE MovimentacaoEstoque
                SET Tipo = CASE Tipo
                    WHEN '0' THEN 'Entrada'
                    WHEN '1' THEN 'Saída'
                    WHEN '2' THEN 'Ajuste'
                    ELSE Tipo
                END
                """);

            migrationBuilder.AddColumn<string>(
                name: "AjusteAcao",
                table: "MovimentacaoEstoque",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "MovimentacaoEstoque",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnidadeMedida",
                table: "ItensEstoque",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "un");

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Logins", x => x.Id));

            migrationBuilder.CreateIndex(
                name: "IX_Logins_Username",
                table: "Logins",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacaoEstoque_ItensEstoque_ItemEstoqueId",
                table: "MovimentacaoEstoque",
                column: "ItemEstoqueId",
                principalTable: "ItensEstoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentacaoEstoque_ItensEstoque_ItemEstoqueId",
                table: "MovimentacaoEstoque");

            migrationBuilder.DropTable(name: "Logins");
            migrationBuilder.DropColumn(name: "UnidadeMedida", table: "ItensEstoque");
            migrationBuilder.DropColumn(name: "AjusteAcao", table: "MovimentacaoEstoque");
            migrationBuilder.DropColumn(name: "Local", table: "MovimentacaoEstoque");

            migrationBuilder.AddColumn<long>(
                name: "ItensEstoqueId",
                table: "MovimentacaoEstoque",
                type: "bigint",
                nullable: true);
            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoEstoque_ItensEstoqueId",
                table: "MovimentacaoEstoque",
                column: "ItensEstoqueId");
            migrationBuilder.AddForeignKey(
                name: "FK_MovimentacaoEstoque_ItensEstoque_ItensEstoqueId",
                table: "MovimentacaoEstoque",
                column: "ItensEstoqueId",
                principalTable: "ItensEstoque",
                principalColumn: "Id");

            migrationBuilder.Sql("""
                UPDATE MovimentacaoEstoque
                SET Tipo = CASE Tipo
                    WHEN 'Entrada' THEN '0'
                    WHEN 'Saída' THEN '1'
                    WHEN 'Saida' THEN '1'
                    WHEN 'Ajuste' THEN '2'
                    ELSE '0'
                END
                """);
            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "MovimentacaoEstoque",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
