using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SL_StockTrade.Migrations
{
    public partial class createbasicnodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusinessName = table.Column<string>(nullable: false),
                    InChargePerson = table.Column<string>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Web = table.Column<string>(nullable: true),
                    PlatformCharge = table.Column<double>(nullable: false),
                    RegistredDate = table.Column<string>(nullable: false),
                    BannerImg = table.Column<string>(nullable: true),
                    ProfileImg = table.Column<string>(nullable: true),
                    SalesGoodType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sellers");
        }
    }
}
