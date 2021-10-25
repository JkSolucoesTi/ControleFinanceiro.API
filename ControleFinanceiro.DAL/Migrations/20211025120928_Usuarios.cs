using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.DAL.Migrations
{
    public partial class Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "69e810c5-02c1-4618-8072-aa9b1ccdc83f");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "77587423-7d1b-4ac4-91be-6ccb35a7bd6f");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Foto",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "21a7980c-6b9f-4d38-9406-08f8cd296286", "355598b1-08f8-4ed3-8ea1-956acefb1f5b", "Administrador do sistema", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "cd28c4b8-2bf3-4274-ab6a-b3102d643090", "4afc2b9c-8836-4c9c-8fe5-af74b50396e6", "Usuário do sistema", "Usuario", "USUARIO" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "21a7980c-6b9f-4d38-9406-08f8cd296286");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "cd28c4b8-2bf3-4274-ab6a-b3102d643090");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Foto",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "69e810c5-02c1-4618-8072-aa9b1ccdc83f", "2e164ab2-80b3-4f65-88e0-3da292314e08", "Administrador do sistema", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "77587423-7d1b-4ac4-91be-6ccb35a7bd6f", "c2c8c388-0ef4-4800-9ae1-4df6260d1e0f", "Usuário do sistema", "Usuario", "USUARIO" });
        }
    }
}
