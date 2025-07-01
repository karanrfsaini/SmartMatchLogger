using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMatchLogger.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMatchModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShotRating",
                table: "Match",
                newName: "VolleyRating");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Match",
                newName: "Winner");

            migrationBuilder.AddColumn<int>(
                name: "BackhandRating",
                table: "Match",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ForehandRating",
                table: "Match",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MatchNotes",
                table: "Match",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayStyle",
                table: "Match",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ServeRating",
                table: "Match",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackhandRating",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "ForehandRating",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "MatchNotes",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "PlayStyle",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "ServeRating",
                table: "Match");

            migrationBuilder.RenameColumn(
                name: "Winner",
                table: "Match",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "VolleyRating",
                table: "Match",
                newName: "ShotRating");
        }
    }
}
