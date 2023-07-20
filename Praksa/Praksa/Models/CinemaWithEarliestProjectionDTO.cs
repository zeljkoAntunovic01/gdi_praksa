namespace Praksa.Models
{
    public record CinemaWithEarliestProjectionModel(
        long CinemaId,
        string CinemaName,
        double CinemaLatitude,
        double CinemaLongitude,
        string CinemaAdress,
        string MovieTitle,
        DateTime ProjectionDateTime,
        string GenreName,
        int RunTime,
        string ProjectionType,
        string fetchTime
        );
    
}
