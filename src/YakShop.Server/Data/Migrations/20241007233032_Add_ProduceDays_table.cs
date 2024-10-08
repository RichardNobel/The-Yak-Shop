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
            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AgeNextShave",
                table: "HerdMembers",
                type: "decimal(4,2)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.CreateTable(
                name: "ProduceDays",
                columns: table => new
                {
                    DayNumber = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AgeNextShave",
                table: "HerdMembers");
        }
    }
}
