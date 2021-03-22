using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace Project05.Migrations {
    [ExcludeFromCodeCoverage]
    public partial class SqliteInit : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DocumentPath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Documents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TokenText = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Tokens", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentToken",
                columns: table => new {
                    DocumentsID = table.Column<int>(type: "INTEGER", nullable: false),
                    TokensID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_DocumentToken", x => new { x.DocumentsID, x.TokensID });
                    table.ForeignKey(
                        name: "FK_DocumentToken_Documents_DocumentsID",
                        column: x => x.DocumentsID,
                        principalTable: "Documents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentToken_Tokens_TokensID",
                        column: x => x.TokensID,
                        principalTable: "Tokens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentPath",
                table: "Documents",
                column: "DocumentPath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentToken_TokensID",
                table: "DocumentToken",
                column: "TokensID");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_TokenText",
                table: "Tokens",
                column: "TokenText",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "DocumentToken");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
