using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLCHelperStation.Migrations.TestDb
{
    /// <inheritdoc />
    public partial class UpdateS7Config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "S7Name",
                table: "T_S7Config",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "S7Name",
                table: "T_S7Config");
        }
    }
}
