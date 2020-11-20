using Microsoft.EntityFrameworkCore.Migrations;

namespace MT.OnlineRestaurant.DataLayer.Migrations
{
    public partial class _20200522_Added_Restarant_Menu_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblMenu_tblCuisineID",
                table: "tblMenu");

            migrationBuilder.RenameColumn(
                name: "tblCuisineID",
                table: "tblMenu",
                newName: "tblRestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_tblMenu_tblCuisineID",
                table: "tblMenu",
                newName: "IX_tblMenu_tblRestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblMenu_tblRestaurantID",
                table: "tblMenu",
                column: "tblRestaurantId",
                principalTable: "tblRestaurant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblMenu_tblRestaurantID",
                table: "tblMenu");

            migrationBuilder.RenameColumn(
                name: "tblRestaurantId",
                table: "tblMenu",
                newName: "tblCuisineID");

            migrationBuilder.RenameIndex(
                name: "IX_tblMenu_tblRestaurantId",
                table: "tblMenu",
                newName: "IX_tblMenu_tblCuisineID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblMenu_tblCuisineID",
                table: "tblMenu",
                column: "tblCuisineID",
                principalTable: "tblCuisine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
