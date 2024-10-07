using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YakShop.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_ProduceDays_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProduceDays",
                columns: table => new
                {
                    DayNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Milk = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    Skins = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduceDays", x => x.DayNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduceDays");
        }
    }
}
