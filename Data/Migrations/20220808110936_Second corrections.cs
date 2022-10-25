using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Secondcorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser1_Groups_ParticipantsGroupId",
                table: "GroupUser1");

            migrationBuilder.DropForeignKey(
                name: "FK_RideUser_Rides_ParticipantsRidesId",
                table: "RideUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_OwnerUserId",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "OwnerUserId",
                table: "Routes",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_OwnerUserId",
                table: "Routes",
                newName: "IX_Routes_OwnerId");

            migrationBuilder.RenameColumn(
                name: "ParticipantsRidesId",
                table: "RideUser",
                newName: "RidesId");

            migrationBuilder.RenameIndex(
                name: "IX_RideUser_ParticipantsRidesId",
                table: "RideUser",
                newName: "IX_RideUser_RidesId");

            migrationBuilder.RenameColumn(
                name: "ParticipantsGroupId",
                table: "GroupUser1",
                newName: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser1_Groups_GroupsId",
                table: "GroupUser1",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RideUser_Rides_RidesId",
                table: "RideUser",
                column: "RidesId",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_OwnerId",
                table: "Routes",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser1_Groups_GroupsId",
                table: "GroupUser1");

            migrationBuilder.DropForeignKey(
                name: "FK_RideUser_Rides_RidesId",
                table: "RideUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_OwnerId",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Routes",
                newName: "OwnerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_OwnerId",
                table: "Routes",
                newName: "IX_Routes_OwnerUserId");

            migrationBuilder.RenameColumn(
                name: "RidesId",
                table: "RideUser",
                newName: "ParticipantsRidesId");

            migrationBuilder.RenameIndex(
                name: "IX_RideUser_RidesId",
                table: "RideUser",
                newName: "IX_RideUser_ParticipantsRidesId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "GroupUser1",
                newName: "ParticipantsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser1_Groups_ParticipantsGroupId",
                table: "GroupUser1",
                column: "ParticipantsGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RideUser_Rides_ParticipantsRidesId",
                table: "RideUser",
                column: "ParticipantsRidesId",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_OwnerUserId",
                table: "Routes",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
