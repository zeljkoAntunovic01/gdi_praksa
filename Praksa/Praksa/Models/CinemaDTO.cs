namespace Praksa.Models
{
    public record GetCinemasResponse(
        List<CinemaModel> Cinemas);
    public record CinemaModel(
        long Id,
        string Name,
        double Latitude,
        double Longitude,
        string Adress);
}
