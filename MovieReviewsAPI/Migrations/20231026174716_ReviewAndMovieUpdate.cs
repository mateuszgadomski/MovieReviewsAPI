using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewsAPI.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAndMovieUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatedById",
                table: "Reviews",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CreatedById",
                table: "Movies",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_CreatedById",
                table: "Movies",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CreatedById",
                table: "Reviews",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_CreatedById",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CreatedById",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CreatedById",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CreatedById",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Movies");
        }
    }
}
