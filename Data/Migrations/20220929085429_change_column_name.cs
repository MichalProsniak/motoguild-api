using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class change_column_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stop",
                table: "Events",
                newName: "StopDate");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Events",
                newName: "StartDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StopDate",
                table: "Events",
                newName: "Stop");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Events",
                newName: "Start");
        }
    }
}
