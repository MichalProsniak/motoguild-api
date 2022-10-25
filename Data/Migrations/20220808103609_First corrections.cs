using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Firstcorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Users_UsersId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RideUser_Rides_RidesId",
                table: "RideUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RideUser_Users_UsersId",
                table: "RideUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_UserId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_UserId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "RideUser",
                newName: "ParticipantsRidesId");

            migrationBuilder.RenameColumn(
                name: "RidesId",
                table: "RideUser",
                newName: "ParticipantsId");

            migrationBuilder.RenameIndex(
                name: "IX_RideUser_UsersId",
                table: "RideUser",
                newName: "IX_RideUser_ParticipantsRidesId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "GroupUser",
                newName: "PendingUsersId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "GroupUser",
                newName: "PendingGroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_UsersId",
                table: "GroupUser",
                newName: "IX_GroupUser_PendingUsersId");

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserId",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Rides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupUser1",
                columns: table => new
                {
                    ParticipantsGroupId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser1", x => new { x.ParticipantsGroupId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_GroupUser1_Groups_ParticipantsGroupId",
                        column: x => x.ParticipantsGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser1_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_OwnerUserId",
                table: "Routes",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_OwnerId",
                table: "Rides",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OwnerId",
                table: "Groups",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerId",
                table: "Events",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser1_ParticipantsId",
                table: "GroupUser1",
                column: "ParticipantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_OwnerId",
                table: "Events",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_OwnerId",
                table: "Groups",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_PendingGroupsId",
                table: "GroupUser",
                column: "PendingGroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Users_PendingUsersId",
                table: "GroupUser",
                column: "PendingUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Users_OwnerId",
                table: "Rides",
                column: "OwnerId",
                principalTable: "Users",
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
                name: "FK_RideUser_Users_ParticipantsId",
                table: "RideUser",
                column: "ParticipantsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_OwnerUserId",
                table: "Routes",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_OwnerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_OwnerId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Groups_PendingGroupsId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_Users_PendingUsersId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Users_OwnerId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_RideUser_Rides_ParticipantsRidesId",
                table: "RideUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RideUser_Users_ParticipantsId",
                table: "RideUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_OwnerUserId",
                table: "Routes");

            migrationBuilder.DropTable(
                name: "GroupUser1");

            migrationBuilder.DropIndex(
                name: "IX_Routes_OwnerUserId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Rides_OwnerId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Groups_OwnerId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Events_OwnerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ParticipantsRidesId",
                table: "RideUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "ParticipantsId",
                table: "RideUser",
                newName: "RidesId");

            migrationBuilder.RenameIndex(
                name: "IX_RideUser_ParticipantsRidesId",
                table: "RideUser",
                newName: "IX_RideUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "PendingUsersId",
                table: "GroupUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "PendingGroupsId",
                table: "GroupUser",
                newName: "GroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_PendingUsersId",
                table: "GroupUser",
                newName: "IX_GroupUser_UsersId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_UserId",
                table: "Routes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Groups_GroupsId",
                table: "GroupUser",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_Users_UsersId",
                table: "GroupUser",
                column: "UsersId",
                principalTable: "Users",
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
                name: "FK_RideUser_Users_UsersId",
                table: "RideUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_UserId",
                table: "Routes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
