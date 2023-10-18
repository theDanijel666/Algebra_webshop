using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Movies.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderModelChangeAndNewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Active", "Description", "Price", "Quantity", "Title" },
                values: new object[,]
                {
                    { 50, true, "Rambo is a 2008 American action film directed by Sylvester Stallone, who co-wrote it based on the character John Rambo created by author David Morrell for his novel First Blood. A sequel to Rambo III (1988) and the fourth installment in the Rambo franchise, it co-stars Julie Benz, Paul Schulze, Matthew Marsden, Graham McTavish, Rey Gallegos, Tim Kang, Jake La Botz, Maung Maung Khin, and Ken Howard. In the film, Rambo leads a group of mercenaries into Burma to rescue Christian missionaries, who have been kidnapped by a local infantry unit.", 10.99m, 10m, "Rambo" },
                    { 51, true, "Rambo: First Blood Part II (also known as Rambo II or First Blood II) is a 1985 American action film directed by George P. Cosmatos and starring Sylvester Stallone, who reprises his role as Vietnam veteran John Rambo. It is the sequel to the 1982 film First Blood, and the second installment in the Rambo franchise. Picking up where the first film left, the sequel is set in the context of the Vietnam War POW/MIA issue; it sees Rambo released from prison by federal order to document the possible existence of POWs in Vietnam, under the belief that he will find nothing, thus enabling the government to sweep the issue under the rug.", 10.99m, 10m, "Rambo 2" },
                    { 52, true, "Rambo III is a 1988 American action film directed by Peter MacDonald and co-written by Sylvester Stallone, who also reprises his role as Vietnam War veteran John Rambo. A sequel to Rambo: First Blood Part II (1985), it is the third installment in the Rambo franchise, followed by Rambo. It was in turn followed by Rambo: Last Blood (2019).", 10.99m, 10m, "Rambo 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 52);
        }
    }
}
