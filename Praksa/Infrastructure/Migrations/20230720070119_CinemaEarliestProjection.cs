using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CinemaEarliestProjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW EarliestProjectionPerCinema AS SELECT CinemaId, CinemaName, MovieTitle, ProjectionDateTime, GenreName, RunTime, ProjectionType
FROM (
  SELECT
	CinemaId,
	dbo.Cinemas.Name AS CinemaName,
	dbo.Movies.Title as MovieTitle,
	dbo.Projections.ProjectionDateTime AS ProjectionDateTime,
	dbo.Genres.Name AS GenreName,
	dbo.Movies.RunTime AS RunTime,
	dbo.ProjectionTypes.Name AS ProjectionType,
    ROW_NUMBER() OVER (PARTITION BY CinemaId ORDER BY ProjectionDateTime) AS rn
  FROM dbo.Projections 
  INNER JOIN dbo.Cinemas ON CinemaId = dbo.Cinemas.Id 
  INNER JOIN dbo.Movies ON MovieId = dbo.Movies.Id
  INNER JOIN dbo.Genres ON dbo.Movies.GenreId = dbo.Genres.Id
  INNER JOIN dbo.ProjectionTypes ON dbo.ProjectionTypes.Id = dbo.Projections.ProjectionTypeId
) RankedProjections
WHERE rn = 1;");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
drop view EarliestProjectionPerCinema;
");

        }
    }
}
