using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class RenameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "DeviceTypes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DeviceTypes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeviceTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeviceTypes");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "DeviceTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DeviceTypes",
                type: "text",
                nullable: true);
        }
    }
}
