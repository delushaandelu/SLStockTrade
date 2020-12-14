using Microsoft.EntityFrameworkCore.Migrations;

namespace SL_StockTrade.Migrations
{
    public partial class addingsellerdevicedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "deviceLocation",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deviceUsername",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipaddress",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deviceLocation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "deviceUsername",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ipaddress",
                table: "AspNetUsers");
        }
    }
}
