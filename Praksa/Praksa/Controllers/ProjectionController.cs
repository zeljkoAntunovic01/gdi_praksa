using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praksa.Models;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Praksa.Controllers
{
    [Route("api/projections")]
    [ApiController]
    public class ProjectionController : ControllerBase
    {
        private readonly MoviesDbContext _dbContext;
        public ProjectionController(MoviesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProjectionModel>>> GetProjections()
        {
            var query = from projection in _dbContext.Projections
                        join cinema in _dbContext.Cinemas
                        on projection.CinemaId equals cinema.Id
                        join movie in _dbContext.Movies
                        on projection.MovieId equals movie.Id
                        join projectionType in _dbContext.ProjectionTypes
                        on projection.ProjectionTypeId equals projectionType.Id
                        select new
                        {
                            projection,
                            movieTitle = movie.Title,
                            cinemaName = cinema.Name,
                            projectionTypeName = projectionType.Name
                        };

            var queryLinq = await _dbContext.Projections.Include(x => x.Movie).Include(x => x.Cinema).Include(x => x.ProjectionType).ToListAsync();
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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieModel>> GetMovie(int id)
        {
            var query = from projection in _dbContext.Projections
                        join cinema in _dbContext.Cinemas
                        on projection.CinemaId equals cinema.Id
                        join movie in _dbContext.Movies
                        on projection.MovieId equals movie.Id
                        join projectionType in _dbContext.ProjectionTypes
                        on projection.ProjectionTypeId equals projectionType.Id
                        select new
                        {
                            projection,
                            movieTitle = movie.Title,
                            cinemaName = cinema.Name,
                            projectionTypeName = projectionType.Name
                        };

            var results = await query.ToListAsync();
            var result = results[0];

            DateTime projectionDateTime = result.projection.ProjectionDateTime;
            string pDateTime = DateOnly.FromDateTime(projectionDateTime).ToString("MM/dd/yyyy HH:mm:ss");
            // Now, the dateOnly variable holds the date portion without the time component.

            ProjectionModel p = new ProjectionModel
            (
                result.projection.Id,
                result.projection.MovieId,
                result.movieTitle,
                result.projection.CinemaId,
                result.cinemaName,
                pDateTime,
                result.projection.ProjectionTypeId,
                result.projectionTypeName
            );

            return Ok(p);
        }
        [HttpPost("add-projection")]
        public async Task<ActionResult> AddProjection([FromBody] ProjectionModel projectionModel)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Name == projectionModel.CinemaName);
                if (cinema == null)
                {
                    return BadRequest("Cinema doesn't exist");
                }
                var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Title == projectionModel.MovieTitle);
                if (movie == null)
                {
                    return BadRequest("Movie doesn't exist");
                }
                var ptype = await _dbContext.ProjectionTypes.FirstOrDefaultAsync(x => x.Name == projectionModel.ProjectionTypeName);
                if (ptype == null)
                {
                    return BadRequest("Ptype doesn't exist");
                }
                DateTime projectionDateTime = DateTime.Parse(projectionModel.DateTimeProjection);

                var projection = new Projection { MovieId = movie.Id, CinemaId = cinema.Id, ProjectionDateTime = projectionDateTime, ProjectionTypeId = ptype.Id };

                _dbContext.Projections.Add(projection);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }

            return Ok();
        }
        [HttpPut("update-projection")]
        public async Task<ActionResult<ProjectionModel>> UpdateProjection([FromBody] ProjectionModel projectionModel)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var projection = await _dbContext.Projections.Include(t => t.Cinema).Include(t => t.Movie).FirstOrDefaultAsync(x => x.Id == projectionModel.Id);

            if (projection == null)
            {
                return BadRequest("Projection doesn't exist");
            }

            var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Name == projectionModel.CinemaName);
            long cinemaId;

            if (cinema == null)
            {
                return BadRequest("Cinema doesn't exist");
            }
            else
            {
                cinemaId = cinema.Id;
            }

            var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Title == projectionModel.MovieTitle);
            long movieId;

            if (movie == null)
            {
                return BadRequest("Movie doesn't exist");
            }
            else
            {
                movieId = movie.Id;
            }
            var pType = await _dbContext.ProjectionTypes.FirstOrDefaultAsync(x => x.Name == projectionModel.ProjectionTypeName);
            long pTypeId;

            if (pType == null)
            {
                return BadRequest("pType doesn't exist");
            }
            else
            {
                pTypeId = pType.Id;
            }
            DateTime projectionDateTime = DateTime.Parse(projectionModel.DateTimeProjection);
            projection.ProjectionDateTime = projectionDateTime;
            projection.MovieId = movieId;
            projection.CinemaId = cinemaId;
            projection.ProjectionTypeId = pTypeId;

            await _dbContext.SaveChangesAsync();

            var updatedProjectionModel = new ProjectionModel
            (
                projection.Id,
                projection.MovieId,
                movie.Title,

                projection.CinemaId,
                cinema.Name,
                projectionModel.DateTimeProjection,
                projection.ProjectionTypeId,
                pType.Name
            );

            return Ok(updatedProjectionModel);
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteProjection(int id)
        {
            var projection = await _dbContext.Projections.FirstAsync(x => x.Id == id);
            if (projection == null)
            {
                return BadRequest("Projection doesn't exist!");
            }

            _dbContext.Projections.Remove(projection);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
