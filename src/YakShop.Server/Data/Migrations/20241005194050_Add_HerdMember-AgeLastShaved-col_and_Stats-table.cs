using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YakShop.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_HerdMemberAgeLastShavedcol_and_Statstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "HerdMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HerdMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Age",
                table: "HerdMembers",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "HerdMembers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<decimal>(
                name: "AgeLastShaved",
                table: "HerdMembers",
                type: "decimal(4,2)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropColumn(
                name: "AgeLastShaved",
                table: "HerdMembers");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "HerdMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HerdMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Age",
                table: "HerdMembers",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "HerdMembers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);
        }
    }
}