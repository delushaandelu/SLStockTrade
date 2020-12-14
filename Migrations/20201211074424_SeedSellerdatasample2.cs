using Microsoft.EntityFrameworkCore.Migrations;

namespace SL_StockTrade.Migrations
{
    public partial class SeedSellerdatasample2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 2,
                column: "InChargePerson",
                value: "Ruchiragna");

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "Id", "Address", "BannerImg", "BusinessName", "Country", "Description", "Email", "InChargePerson", "Location", "Mobile", "Password", "PlatformCharge", "ProfileImg", "RegistredDate", "SalesGoodType", "Telephone", "Username", "Web" },
                values: new object[] { 3, "Downtown center", "img", "Ruchiragna Masala Shop", 1, "some textsome textsome textsome textsome textsome text", "delushaan@outlook.com", "Ruchiragna", "Downtown", "0000000000", null, 5.7999999999999998, "prof", "12/11/2020 12:00:00 AM", 1, "1111111111", null, "www.google.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Sellers",
                keyColumn: "Id",
                keyValue: 2,
                column: "InChargePerson",
                value: "Delushaan Delu");
        }
    }
}
