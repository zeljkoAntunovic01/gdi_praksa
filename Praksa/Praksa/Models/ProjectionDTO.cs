namespace Praksa.Models
{
    public record GetProjectionsResponse(
        List<ProjectionModel> Projections);
    public record ProjectionModel(
        long Id,
        long MovieId,
        string MovieTitle,
        long CinemaId,
        string CinemaName,
        string DateTimeProjection,
        long ProjectionTypeId,
        string ProjectionTypeName);
}
