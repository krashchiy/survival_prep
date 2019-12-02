using Microsoft.EntityFrameworkCore.Migrations;

namespace SurvivalPrep.Migrations.UsersRolesDBMigrations
{
    public partial class AddDBReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDisaster_Disasters_DisasterId",
                table: "ItemDisaster");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemDisaster_Items_ItemId",
                table: "ItemDisaster");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInstance_AspNetUsers_ApplicationUserID",
                table: "ItemInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInstance_Items_ItemId",
                table: "ItemInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemInstance",
                table: "ItemInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemDisaster",
                table: "ItemDisaster");

            migrationBuilder.RenameTable(
                name: "ItemInstance",
                newName: "ItemInstances");

            migrationBuilder.RenameTable(
                name: "ItemDisaster",
                newName: "ItemDisasters");

            migrationBuilder.RenameIndex(
                name: "IX_ItemInstance_ApplicationUserID",
                table: "ItemInstances",
                newName: "IX_ItemInstances_ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemDisaster_DisasterId",
                table: "ItemDisasters",
                newName: "IX_ItemDisasters_DisasterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemInstances",
                table: "ItemInstances",
                columns: new[] { "ItemId", "ApplicationUserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemDisasters",
                table: "ItemDisasters",
                columns: new[] { "ItemId", "DisasterId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDisasters_Disasters_DisasterId",
                table: "ItemDisasters",
                column: "DisasterId",
                principalTable: "Disasters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDisasters_Items_ItemId",
                table: "ItemDisasters",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInstances_AspNetUsers_ApplicationUserID",
                table: "ItemInstances",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInstances_Items_ItemId",
                table: "ItemInstances",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDisasters_Disasters_DisasterId",
                table: "ItemDisasters");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemDisasters_Items_ItemId",
                table: "ItemDisasters");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInstances_AspNetUsers_ApplicationUserID",
                table: "ItemInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInstances_Items_ItemId",
                table: "ItemInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemInstances",
                table: "ItemInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemDisasters",
                table: "ItemDisasters");

            migrationBuilder.RenameTable(
                name: "ItemInstances",
                newName: "ItemInstance");

            migrationBuilder.RenameTable(
                name: "ItemDisasters",
                newName: "ItemDisaster");

            migrationBuilder.RenameIndex(
                name: "IX_ItemInstances_ApplicationUserID",
                table: "ItemInstance",
                newName: "IX_ItemInstance_ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemDisasters_DisasterId",
                table: "ItemDisaster",
                newName: "IX_ItemDisaster_DisasterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemInstance",
                table: "ItemInstance",
                columns: new[] { "ItemId", "ApplicationUserID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemDisaster",
                table: "ItemDisaster",
                columns: new[] { "ItemId", "DisasterId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDisaster_Disasters_DisasterId",
                table: "ItemDisaster",
                column: "DisasterId",
                principalTable: "Disasters",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDisaster_Items_ItemId",
                table: "ItemDisaster",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInstance_AspNetUsers_ApplicationUserID",
                table: "ItemInstance",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInstance_Items_ItemId",
                table: "ItemInstance",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
