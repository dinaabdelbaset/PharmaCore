using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstablishedDate",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Branches",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Branches",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerEmail",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerPhone",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpeningHours",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phones",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Branches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalArea",
                table: "Branches",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "EstablishedDate",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ManagerEmail",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "ManagerPhone",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "OpeningHours",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Phones",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "TotalArea",
                table: "Branches");
        }
    }
}
