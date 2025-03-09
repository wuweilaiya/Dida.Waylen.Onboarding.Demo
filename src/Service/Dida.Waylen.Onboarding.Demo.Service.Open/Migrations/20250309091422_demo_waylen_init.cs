using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Migrations
{
    /// <inheritdoc />
    public partial class demo_waylen_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false, comment: "主键")
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "酒店名称"),
                    Address_Latitude = table.Column<int>(type: "INTEGER", nullable: false, comment: "纬度"),
                    Address_Longitude = table.Column<int>(type: "INTEGER", nullable: false, comment: "经度"),
                    Address_Country = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "国家"),
                    Address_CountryCode = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "国家编码"),
                    Address_Province = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "省份"),
                    Address_ProvinceCode = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "省份编码"),
                    Address_City = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "城市"),
                    Address_CityCode = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "城市编码"),
                    Address_Region = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "区县"),
                    Address_RegionCode = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "区县编码"),
                    Address_Street = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "街道"),
                    Address_Detail = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false, comment: "详细地址"),
                    Address_PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, comment: "邮政编码"),
                    Contact_PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "联系电话"),
                    Contact_Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "联系邮箱"),
                    Contact_Website = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false, comment: "网站"),
                    Image_Url = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false, comment: "主图"),
                    Image_Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "主图描述"),
                    Image_Attached = table.Column<string>(type: "TEXT", nullable: false, comment: "附图"),
                    HotelStarRating = table.Column<int>(type: "INTEGER", nullable: false, comment: "酒店星级"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false, comment: "酒店描述"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "创建时间"),
                    CreateUserId = table.Column<Guid>(type: "TEXT", nullable: false, comment: "创建者用户ID"),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "最后修改时间"),
                    UpdateUserId = table.Column<Guid>(type: "TEXT", nullable: false, comment: "最后修改者用户ID"),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false, comment: "主键")
                        .Annotation("Sqlite:Autoincrement", true),
                    HotelId = table.Column<long>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "房间号码"),
                    Type = table.Column<int>(type: "INTEGER", nullable: false, comment: "房型"),
                    TypeDescription = table.Column<string>(type: "TEXT", nullable: false, comment: "房型描述"),
                    BedType = table.Column<int>(type: "INTEGER", nullable: false, comment: "床型"),
                    BedTypeDescription = table.Column<string>(type: "TEXT", nullable: false, comment: "床型描述"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false, comment: "房间描述"),
                    Image_Url = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false, comment: "主图"),
                    Image_Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "主图描述"),
                    Image_Attached = table.Column<string>(type: "TEXT", nullable: false, comment: "附图"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "创建时间"),
                    CreateUserId = table.Column<Guid>(type: "TEXT", nullable: false, comment: "创建者用户ID"),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "最后修改时间"),
                    UpdateUserId = table.Column<Guid>(type: "TEXT", nullable: false, comment: "最后修改者用户ID"),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false, comment: "是否删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_HotelStarRating",
                table: "Hotel",
                column: "HotelStarRating");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_IsDeleted",
                table: "Hotel",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_Name",
                table: "Hotel",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Room_BedType",
                table: "Room",
                column: "BedType");

            migrationBuilder.CreateIndex(
                name: "IX_Room_HotelId",
                table: "Room",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_IsDeleted",
                table: "Room",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Type",
                table: "Room",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Hotel");
        }
    }
}
