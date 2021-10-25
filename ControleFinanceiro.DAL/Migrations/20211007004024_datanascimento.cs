using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.DAL.Migrations
{
    public partial class datanascimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "048b4dab-49f8-4bc0-987b-69a1ee47b8bb");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "c231a077-0584-46c8-8b99-6627576c3a09");

            migrationBuilder.AddColumn<string>(
                name: "DataNascimento",
                table: "Cadastro",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "69e810c5-02c1-4618-8072-aa9b1ccdc83f", "2e164ab2-80b3-4f65-88e0-3da292314e08", "Administrador do sistema", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "77587423-7d1b-4ac4-91be-6ccb35a7bd6f", "c2c8c388-0ef4-4800-9ae1-4df6260d1e0f", "Usuário do sistema", "Usuario", "USUARIO" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "69e810c5-02c1-4618-8072-aa9b1ccdc83f");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "77587423-7d1b-4ac4-91be-6ccb35a7bd6f");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Cadastro");

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "c231a077-0584-46c8-8b99-6627576c3a09", "73e55c3b-6dee-4e8b-b648-daec7bb489b2", "Administrador do sistema", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "048b4dab-49f8-4bc0-987b-69a1ee47b8bb", "0b3e5ea2-6d3f-401f-849d-a364b32f5bb1", "Usuário do sistema", "Usuario", "USUARIO" });
        }
    }
}
