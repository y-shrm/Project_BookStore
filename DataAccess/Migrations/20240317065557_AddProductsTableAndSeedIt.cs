using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsTableAndSeedIt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Allen Kim Lang", "World in a Bottle is a captivating science fiction novella authored by using Allen Kim Lang.\r\n The story immerses readers in a world of clinical marvel and moral dilemmas.\r\nSet in a futuristic society the narrative follows Dr. Martin Hale a notable scientist with a imaginative and prescient\r\n to create a self-contained miniature universe inside a tumbler bottle.", 99.0, "World in a Bottle" },
                    { 2, "Richard A. Proctor", "Myths and Marvels of Astronomy is a fascinating paintings authored with the aid of Richard A.\r\n Proctor a prominent 19th-century British astronomer and writer. This ebook takes readers on an enlightening journey thru the fascinating world \r\nof astronomy debunking myths even as revealing the awe-inspiring marvels of the universe. ", 299.0, "Myths And Marvels Of\r\nAstronomy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
