using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeLong.Migrations
{
    /// <inheritdoc />
    public partial class Seconds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Informs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TovarNomi = table.Column<string>(type: "text", nullable: false),
                    Soni = table.Column<int>(type: "integer", nullable: false),
                    SotibOlishNarxi = table.Column<decimal>(type: "numeric", nullable: false),
                    KirimSummasi = table.Column<decimal>(type: "numeric", nullable: false),
                    Foizi = table.Column<int>(type: "integer", nullable: false),
                    SotishNarxi = table.Column<decimal>(type: "numeric", nullable: false),
                    SotishSummasi = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kirims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ombornomi = table.Column<string>(type: "text", nullable: false),
                    Yetkazuvchi = table.Column<string>(type: "text", nullable: false),
                    JamiSoni = table.Column<int>(type: "integer", nullable: false),
                    Jaminarxi = table.Column<int>(type: "integer", nullable: false),
                    InformId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kirims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kirims_Informs_InformId",
                        column: x => x.InformId,
                        principalTable: "Informs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kirims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kirims_InformId",
                table: "Kirims",
                column: "InformId");

            migrationBuilder.CreateIndex(
                name: "IX_Kirims_RoleId",
                table: "Kirims",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kirims");

            migrationBuilder.DropTable(
                name: "Informs");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
