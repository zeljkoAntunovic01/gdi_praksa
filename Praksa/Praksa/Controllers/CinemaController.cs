using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praksa.Models;

namespace Praksa.Controllers
{
    [Route("api/cinemas")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly MoviesDbContext _dbContext;
        public CinemaController(MoviesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<CinemaModel>>> GetCinemas()
        {
            var cinemas = await _dbContext.Cinemas.Select(x => new CinemaModel(x.Id, x.Name, x.Latitude, x.Longitude, x.Adress)).ToListAsync();

            return Ok(cinemas);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CinemaModel>> GetCinema(int id)
        {
            var cinemaBase = await _dbContext.Cinemas.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (cinemaBase == null)
            {
                return BadRequest("Cinema doesn't exist");
            }
            var cinema = new CinemaModel(cinemaBase.Id, cinemaBase.Name, cinemaBase.Latitude, cinemaBase.Longitude, cinemaBase.Adress);
            return Ok(cinema);
        }
        [HttpPost("add-cinema")]
        public async Task<ActionResult> AddCinema([FromBody] CinemaModel cinemaModel)
        {

            var cinema = new Cinema { Name = cinemaModel.Name , Latitude = cinemaModel.Latitude, Longitude = cinemaModel.Longitude, Adress = cinemaModel.Adress};

            _dbContext.Cinemas.Add(cinema);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("update-cinema")]
        public async Task<ActionResult<CinemaModel>> UpdateCinema([FromBody] CinemaModel cinemaModel)
        {
            var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Id == cinemaModel.Id);
            if (cinema == null)
            {
                return BadRequest("Cinema doesn't exist");
            }

            cinema.Name = cinemaModel.Name;
            cinema.Latitude = cinemaModel.Latitude;
            cinema.Longitude = cinemaModel.Longitude;
            cinema.Adress = cinemaModel.Adress;

            await _dbContext.SaveChangesAsync();

            return Ok(cinemaModel);
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteCinema(int id)
        {
            var cinema = await _dbContext.Cinemas.FirstAsync(x => x.Id == id);
            if (cinema == null)
            {
                return BadRequest("Cinema doesn't exist!");
            }


            _dbContext.Cinemas.Remove(cinema);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("projections/{id:int}")]
        public async Task<ActionResult<List<ProjectionModel>>> getProjectionsInCinema(int id)
        {
            var query = from projection in _dbContext.Projections
                        join projectionType in _dbContext.ProjectionTypes
                        on projection.ProjectionTypeId equals projectionType.Id
                        join cinema in _dbContext.Cinemas
                        on projection.CinemaId equals cinema.Id
                        join movie in _dbContext.Movies
                        on projection.MovieId equals movie.Id
                        where cinema.Id == id
                        select new
                        {
                            projection,
                            movieTitle = movie.Title,
                            cinemaName = cinema.Name,
                            projectionTypeName = projectionType.Name
                        };
            var queryLinq = await _dbContext.Projections.Include(x => x.Movie).Include(x => x.Cinema).Include(x => x.ProjectionType).Where(x => x.CinemaId == id).ToListAsync();
            var queryLinq = await _dbContext.Projections.Include(x => x.Movie).Include(x => x.Cinema).Include(x => x.ProjectionType).FirstAsync(x => x.CinemaId == id);
            var results = await query.ToListAsync();
            List<ProjectionModel> projections = new List<ProjectionModel>();
            foreach (var result in results)
            {
                DateTime d = result.projection.ProjectionDateTime;
                string dateTime = d.ToString("MM/dd/yyyy HH:mm:ss");
                ProjectionModel p = new ProjectionModel
                (
                    result.projection.Id,
                    result.projection.MovieId,
                    result.movieTitle,
                    result.projection.CinemaId,
                    result.cinemaName,
                    dateTime,
                    result.projection.ProjectionTypeId,
                    result.projectionTypeName
                );
                projections.Add(p);
            }
            return Ok(projections);

        }

    }
}
