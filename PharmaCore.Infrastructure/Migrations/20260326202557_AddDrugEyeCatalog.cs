using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugEyeCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrugEyeActiveIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugEyeActiveIngredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugEyeUsages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugEyeUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugEyeDrugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TradeNameEn = table.Column<string>(type: "text", nullable: false),
                    TradeNameAr = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    PublicPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    OldPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ActiveIngredientId = table.Column<Guid>(type: "uuid", nullable: true),
                    UsageId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugEyeDrugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugEyeDrugs_DrugEyeActiveIngredients_ActiveIngredientId",
                        column: x => x.ActiveIngredientId,
                        principalTable: "DrugEyeActiveIngredients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DrugEyeDrugs_DrugEyeUsages_UsageId",
                        column: x => x.UsageId,
                        principalTable: "DrugEyeUsages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugEyeDrugs_ActiveIngredientId",
                table: "DrugEyeDrugs",
                column: "ActiveIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugEyeDrugs_UsageId",
                table: "DrugEyeDrugs",
                column: "UsageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugEyeDrugs");

            migrationBuilder.DropTable(
                name: "DrugEyeActiveIngredients");

            migrationBuilder.DropTable(
                name: "DrugEyeUsages");
        }
    }
}
