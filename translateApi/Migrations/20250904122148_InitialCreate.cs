using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace translateApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TranslatedText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FromLanguage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ToLanguage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "CreatedAt", "FromLanguage", "IsActive", "OriginalText", "ToLanguage", "TranslatedText", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), "en", true, "hello", "vi", "xin chào", null },
                    { 2, new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), "en", true, "goodbye", "vi", "tạm biệt", null },
                    { 3, new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), "en", true, "thank you", "vi", "cảm ơn", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translation_Search",
                table: "Translations",
                columns: new[] { "OriginalText", "FromLanguage", "ToLanguage" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translations");
        }
    }
}
