using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReportLayouts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportLayouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrgId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    LayoutXml = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportLayouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportLayouts_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportLayouts_OrgId_ReportName",
                table: "ReportLayouts",
                columns: new[] { "OrgId", "ReportName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportLayouts");
        }
    }
}
