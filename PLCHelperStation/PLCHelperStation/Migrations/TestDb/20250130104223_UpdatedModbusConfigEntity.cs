using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLCHelperStation.Migrations.TestDb
{
    /// <inheritdoc />
    public partial class UpdatedModbusConfigEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "T_ModbusConfig",
                newName: "StartAddress");

            migrationBuilder.AddColumn<string>(
                name: "FunctionCode",
                table: "T_ModbusConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Num",
                table: "T_ModbusConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PLCName",
                table: "T_ModbusConfig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SlaveId",
                table: "T_ModbusConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FunctionCode",
                table: "T_ModbusConfig");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "T_ModbusConfig");

            migrationBuilder.DropColumn(
                name: "PLCName",
                table: "T_ModbusConfig");

            migrationBuilder.DropColumn(
                name: "SlaveId",
                table: "T_ModbusConfig");

            migrationBuilder.RenameColumn(
                name: "StartAddress",
                table: "T_ModbusConfig",
                newName: "Name");
        }
    }
}
