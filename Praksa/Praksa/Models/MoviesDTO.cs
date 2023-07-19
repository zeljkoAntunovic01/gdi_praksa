namespace Praksa.Models
{
    public record GetMoviesResponse(
        List<MovieModel> Movies);
    public record MovieModel(
        long Id,
        string Title,
        string ReleaseDate,
        long GenreId,
        int RunTime,
        string GenreName);
    
}
