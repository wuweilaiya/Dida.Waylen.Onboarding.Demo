using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Migrations
{
    /// <inheritdoc />
    public partial class demo_waylen_add_roomnumberUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Room_HotelId",
                table: "Room");

            migrationBuilder.CreateIndex(
                name: "IX_Room_HotelId_Number",
                table: "Room",
                columns: new[] { "HotelId", "Number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Room_HotelId_Number",
                table: "Room");

            migrationBuilder.CreateIndex(
                name: "IX_Room_HotelId",
                table: "Room",
                column: "HotelId");
        }
    }
}
