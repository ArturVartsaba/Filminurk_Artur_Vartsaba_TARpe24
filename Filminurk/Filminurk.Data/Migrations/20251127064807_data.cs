using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoviesActedFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortraitID = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorID);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteLists",
                columns: table => new
                {
                    FavouriteListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListBelongsToUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMovieOrActor = table.Column<bool>(type: "bit", nullable: false),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    ListCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ListModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ListDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReported = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteLists", x => x.FavouriteListID);
                });

            migrationBuilder.CreateTable(
                name: "FilesToApi",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExistingFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPoster = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesToApi", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "FilesToDatabase",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesToDatabase", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstPublished = table.Column<DateOnly>(type: "date", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Actors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRating = table.Column<double>(type: "float", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: true),
                    EntryCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EntryModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FavouriteListID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_FavouriteLists_FavouriteListID",
                        column: x => x.FavouriteListID,
                        principalTable: "FavouriteLists",
                        principalColumn: "FavouriteListID");
                });

            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommenterUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentedScore = table.Column<int>(type: "int", nullable: false),
                    IsHelpful = table.Column<int>(type: "int", nullable: true),
                    IsHarmful = table.Column<int>(type: "int", nullable: true),
                    CommentCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_UserComments_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FavouriteListID",
                table: "Movies",
                column: "FavouriteListID");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_MovieId",
                table: "UserComments",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "FilesToApi");

            migrationBuilder.DropTable(
                name: "FilesToDatabase");

            migrationBuilder.DropTable(
                name: "UserComments");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "FavouriteLists");
        }
    }
}
