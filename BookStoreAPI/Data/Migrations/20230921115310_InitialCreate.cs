using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "Varchar", nullable: false, collation: "nocase"),
                    Author = table.Column<string>(type: "Varchar", nullable: false, collation: "nocase"),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Publisher = table.Column<string>(type: "Varchar", nullable: true, collation: "nocase"),
                    Description = table.Column<string>(type: "Varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.InsertData(
            table: "Book",
            columns: new[] { "Title", "Author", "Year", "Publisher", "Description" },
            values: new object[] { "Storm Front", "Jim Butcher", 2000, "Penguin Putnam", " Harry Blackstone Copperfield Dresden, the only wizard in the phone book, facing down a highly talented but untrained mage who enjoys killing his enemies and anyone who gets in his way by ripping their hearts from their bodies from the inside out, leaving behind an astonishing and astonishingly gory murder." });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Title", "Author", "Year", "Publisher", "Description" },
                values: new object[] { "A little Hatred", "Joe Abercrombie", 2019, "Gollancz", "The chimneys of industry rise over Adua and the world seethes with new opportunities. But old scores run deep as ever. On the blood-soaked borders of Angland, Leo dan Brock struggles to win fame on the battlefield, and defeat the marauding armies of Stour Nightfall." });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Title", "Author", "Year", "Publisher", "Description" },
                values: new object[] { "Lord of the Rings", "J.R.R. Tolkien", 1937, "HarperCollins", "The Lord of the Rings is the saga of a group of sometimes reluctant heroes who set forth to save their world from consummate evil. Its many worlds and creatures were drawn from Tolkien's extensive knowledge of philology and folklore." });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Title", "Author", "Year", "Publisher", "Description" },
                values: new object[] { "Death Masks", "Jim Butcher", 2003, "New American Library", "Harry shares a TV panel with a Vatican priest, Father Vincent, and São Paulo University Professor Don Paolo Ortega, a disguised Red Court Vampire noble. Father Vincent hires Dresden to recover the stolen Shroud of Turin while Ortega challenges Harry to a duel to end the war between the White Council and the Red Court." });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Title", "Author", "Year", "Publisher", "Description" },
                values: new object[] { "Learn Python", "Daniel Ives", 2018, "Dixon & Max", "Learn Python version 1!" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Title", "Author", "Year", "Publisher", "Description" },
                values: new object[] { "Learn Python", "Daniel Ives", 2019, "Dixon & Max", "Learn Python version 2!" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Title", "Author", "Year", "Publisher", "Description" },
                values: new object[] { "Learn Etiquette", "Gilly Sterling", 1750, null, "Learn Eqiquette!" });




        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
           
        }
    }
}
