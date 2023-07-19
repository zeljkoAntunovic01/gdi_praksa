namespace Praksa.Models
{
    public record GetGenresResponse(
        List<GenreModel> Genres);
    public record GenreModel(
        long Id,
        string Name);
}
