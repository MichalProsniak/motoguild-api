using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class changeridestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Rides_RideId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_RideId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "RideId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "EndingPlace",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "StartPlace",
                table: "Rides");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Rides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Groups",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_RouteId",
                table: "Rides",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Routes_RouteId",
                table: "Rides",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Routes_RouteId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_RouteId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Rides");

            migrationBuilder.AddColumn<int>(
                name: "RideId",
                table: "Stops",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndingPlace",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartPlace",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_RideId",
                table: "Stops",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Rides_RideId",
                table: "Stops",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "Id");
        }
    }
}
