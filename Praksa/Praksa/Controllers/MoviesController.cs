using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Praksa.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Drawing.Printing;
using System.Globalization;
using System;

namespace Praksa.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesDbContext _dbContext;
        private readonly HttpClient _httpClient;
        public MoviesController(MoviesDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<ActionResult<List<MovieModel>>> GetMovies()
        {

            var query = from movie in _dbContext.Movies
                          join genre in _dbContext.Genres 
                          on movie.GenreId equals genre.Id
                          select new
                          {
                              movie,
                              GenreName = genre.Name
                          };

            var results = await query.ToListAsync(); 
            List<MovieModel> movies = new List<MovieModel>();
            foreach (var result in results)
            {
                DateTime releaseDate = result.movie.ReleaseDate;
                string dateOnly = DateOnly.FromDateTime(releaseDate).ToString();
                // Now, the dateOnly variable holds the date portion without the time component.

                MovieModel m = new MovieModel
                (
                    result.movie.Id,
                    result.movie.Title,
                    dateOnly,
                    result.movie.GenreId,
                    result.movie.RunTime,
                    result.GenreName
                );
                movies.Add(m);
            }
            return Ok(movies);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieModel>> GetMovie(int id)
        {
            var query = from m in _dbContext.Movies
                        join genre in _dbContext.Genres
                        on m.GenreId equals genre.Id
                        where m.Id == id
                        select new
                        {
                            m,
                            GenreName = genre.Name
                        };

            var results = await query.ToListAsync();
            var result = results[0];

            DateTime releaseDate = result.m.ReleaseDate;
            string dateOnly = DateOnly.FromDateTime(releaseDate).ToString();
            // Now, the dateOnly variable holds the date portion without the time component.

            MovieModel movie = new MovieModel
            (
                result.m.Id,
                result.m.Title,
                dateOnly,
                result.m.GenreId,
                result.m.RunTime,
                result.GenreName
            );

            return Ok(movie);
        }
        [HttpPost("add-movie")]
        public async Task<ActionResult> AddMovie([FromBody] MovieModel movieModel)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Name == movieModel.GenreName);
                if (genre == null)
                {
                    genre = new Genre
                    {
                        Name = movieModel.GenreName
                    };
                    _dbContext.Genres.Add(genre);
                    await _dbContext.SaveChangesAsync();
                }
                DateTime releaseDateTime = DateTime.Parse(movieModel.ReleaseDate);

                var movie = new Movie { Title = movieModel.Title, ReleaseDate = releaseDateTime, GenreId = genre.Id, RunTime = movieModel.RunTime };

                _dbContext.Movies.Add(movie);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            
            return Ok();
        }
        [HttpPut("update-movie")]
        public async Task<ActionResult<MovieModel>> UpdateMovie([FromBody] MovieModel movieModel)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var movie = await _dbContext.Movies.Include(t => t.Genre).FirstOrDefaultAsync(x => x.Id == movieModel.Id);

            if (movie == null)
            {
                return BadRequest("Movie doesn't exist");
            }

            var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Name == movieModel.GenreName);
            long genreId;

            if (genre == null)
            {
                genre = new Genre
                {
                    Name = movieModel.GenreName
                };
                _dbContext.Genres.Add(genre);
                await _dbContext.SaveChangesAsync();
                var addedGenre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Name == movieModel.GenreName);
                genreId = addedGenre.Id;
            }
            else
            {
                genreId = genre.Id;
            }
            DateTime releaseDateTime = DateTime.Parse(movieModel.ReleaseDate);
            movie.Title = movieModel.Title;
            movie.ReleaseDate = releaseDateTime;
            movie.GenreId = genreId;
            movie.RunTime = movieModel.RunTime;

            await _dbContext.SaveChangesAsync();

            var updatedMovieModel = new MovieModel
            (
                movie.Id,
                movie.Title,
                movieModel.ReleaseDate,
                movie.GenreId,
                movie.RunTime,
                movie.Genre.Name
            );

            return Ok(updatedMovieModel);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _dbContext.Movies.FirstAsync(x => x.Id == id);
            if (movie == null)
            {
                return BadRequest("Genre doesn't exist!");
            }

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }

    }
}
