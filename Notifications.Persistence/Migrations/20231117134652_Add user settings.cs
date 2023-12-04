using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addusersettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserSettings_UserSettingsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserSettingsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserSettingsId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettings_Users_Id",
                table: "UserSettings",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettings_Users_Id",
                table: "UserSettings");

            migrationBuilder.AddColumn<Guid>(
                name: "UserSettingsId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserSettingsId",
                table: "Users",
                column: "UserSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserSettings_UserSettingsId",
                table: "Users",
                column: "UserSettingsId",
                principalTable: "UserSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
