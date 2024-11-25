using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeLong.Migrations
{
    /// <inheritdoc />
    public partial class NewKiri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kirims_Informs_InformId",
                table: "Kirims");

            migrationBuilder.DropIndex(
                name: "IX_Kirims_InformId",
                table: "Kirims");

            migrationBuilder.DropColumn(
                name: "InformId",
                table: "Kirims");

            migrationBuilder.AlterColumn<decimal>(
                name: "Jaminarxi",
                table: "Kirims",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "Sana",
                table: "Kirims",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "KirimId",
                table: "Informs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Informs_KirimId",
                table: "Informs",
                column: "KirimId");

            migrationBuilder.AddForeignKey(
                name: "FK_Informs_Kirims_KirimId",
                table: "Informs",
                column: "KirimId",
                principalTable: "Kirims",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informs_Kirims_KirimId",
                table: "Informs");

            migrationBuilder.DropIndex(
                name: "IX_Informs_KirimId",
                table: "Informs");

            migrationBuilder.DropColumn(
                name: "Sana",
                table: "Kirims");

            migrationBuilder.DropColumn(
                name: "KirimId",
                table: "Informs");

            migrationBuilder.AlterColumn<int>(
                name: "Jaminarxi",
                table: "Kirims",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<long>(
                name: "InformId",
                table: "Kirims",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Kirims_InformId",
                table: "Kirims",
                column: "InformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kirims_Informs_InformId",
                table: "Kirims",
                column: "InformId",
                principalTable: "Informs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
