using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ImgUri = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "ImgUri", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1138b2b1-1250-4cb1-9db2-60ad82dda1f7"), null, "some", "g", 100m },
                    { new Guid("27ef6919-b3d6-4341-9c02-177365b55818"), null, "some", "m", 100m },
                    { new Guid("2e3a240d-e162-4b7b-ba2f-d7aa2aa00072"), null, "some", "k", 100m },
                    { new Guid("31de73a4-5ef9-4f4e-9d77-e6cd16b7c34d"), null, "some", "d", 100m },
                    { new Guid("374a06e4-9412-4a64-89ac-bf72d19c8a4f"), null, "some", "i", 100m },
                    { new Guid("3b8b0be8-de0c-4ad2-9868-78481993ce79"), null, "some", "n", 100m },
                    { new Guid("50cc09ba-64ad-43be-9655-569400f570ff"), null, "some", "q", 100m },
                    { new Guid("66801422-d458-4480-b487-db2293d834d0"), null, "some", "o", 100m },
                    { new Guid("67514912-6ffd-4c34-a448-2892437392d6"), null, "some", "c", 100m },
                    { new Guid("9211a52e-b6c6-48d1-ae00-6522c3d98b0c"), null, "some", "a", 100m },
                    { new Guid("92bc6a83-ca5d-4eb3-ba8b-74a3dda6db24"), null, "some", "b", 100m },
                    { new Guid("9c388a2f-efff-4803-92dd-68bba8de6b38"), null, "some", "f", 100m },
                    { new Guid("9c54da3f-cad6-4e32-a72f-a8f791b4e9a6"), null, "some", "j", 100m },
                    { new Guid("a81c9f55-0419-4a56-9e60-20990979a9a4"), null, "some", "l", 100m },
                    { new Guid("b76f21dc-65d0-46be-b021-d168157bd107"), null, "some", "p", 100m },
                    { new Guid("c4cf6d3e-e84a-4dcf-a6e3-bf41e2f51a62"), null, "some", "h", 100m },
                    { new Guid("d3653a37-07c6-4f4b-905c-290ea5b320f0"), null, "some", "e", 100m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Id",
                table: "Product",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
