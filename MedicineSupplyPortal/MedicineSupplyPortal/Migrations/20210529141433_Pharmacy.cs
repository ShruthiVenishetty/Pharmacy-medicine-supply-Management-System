using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicineSupplyPortal.Migrations
{
    public partial class Pharmacy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockSupplies",
                columns: table => new
                {
                    Sno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medicineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    supplyCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSupplies", x => x.Sno);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockSupplies");
        }
    }
}
