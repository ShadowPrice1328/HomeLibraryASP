using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForBooksAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    ID_Author = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Authors__83F33C8BE57BC1D3", x => x.ID_Author);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID_Book = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", maxLength: 1000, nullable: false),
                    Title = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    Image = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    Source = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    Lent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books__DE8DF038131185EC", x => x.ID_Book);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    ID_Genre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genres__7B31A83B8DE42542", x => x.ID_Genre);
                });

            migrationBuilder.CreateTable(
                name: "Books_Authors",
                columns: table => new
                {
                    ID_Book = table.Column<int>(type: "int", nullable: false),
                    ID_Author = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books_Au__76B2C3F0211F3A8B", x => new { x.ID_Book, x.ID_Author });
                    table.ForeignKey(
                        name: "fk_Books_Authors__Authors",
                        column: x => x.ID_Author,
                        principalTable: "Authors",
                        principalColumn: "ID_Author",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_Books_Authors__Books",
                        column: x => x.ID_Book,
                        principalTable: "Books",
                        principalColumn: "ID_Book",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books_Genres",
                columns: table => new
                {
                    ID_Book = table.Column<int>(type: "int", nullable: false),
                    ID_Genre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books_Ge__793EEABB2917F912", x => new { x.ID_Book, x.ID_Genre });
                    table.ForeignKey(
                        name: "fk_Books_Genres__Books",
                        column: x => x.ID_Book,
                        principalTable: "Books",
                        principalColumn: "ID_Book",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_Books_Genres__Genres",
                        column: x => x.ID_Genre,
                        principalTable: "Genres",
                        principalColumn: "ID_Genre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "ID_Author", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "George", "Orwell" },
                    { 2, "Aldous", "Huxley" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "ID_Book", "Description", "Image", "Lent", "Source", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "A dystopian novel by George Orwell.", "1984.jpg", false, "Purchased", "1984", new DateOnly(1949, 1, 1) },
                    { 2, "A dystopian novel by Aldous Huxley.", "BraveNewWorld.jpg", false, "Gifted", "Brave New World", new DateOnly(1932, 1, 1) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "ID_Genre", "Name" },
                values: new object[,]
                {
                    { 1, "Novel" },
                    { 2, "Dystopian" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Authors_ID_Author",
                table: "Books_Authors",
                column: "ID_Author");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Genres_ID_Genre",
                table: "Books_Genres",
                column: "ID_Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books_Authors");

            migrationBuilder.DropTable(
                name: "Books_Genres");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
