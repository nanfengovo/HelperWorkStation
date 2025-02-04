using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLCHelperStation.Migrations.TestDb
{
    /// <inheritdoc />
    public partial class AddIsOpenForModbusConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "T_ModbusConfig",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "T_ModbusConfig");
        }
    }
}
