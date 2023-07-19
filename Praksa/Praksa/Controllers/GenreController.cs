using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Praksa.Models;
using Microsoft.EntityFrameworkCore;
using Core.Entities;


namespace Praksa.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly MoviesDbContext _dbContext;
        public GenreController(MoviesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<GenreModel>>> GetGenres()
        {
            var genres = await _dbContext.Genres.Select(x => new GenreModel(x.Id, x.Name)).ToListAsync();

            return Ok(genres);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreModel>> GetGenre(int id)
        {
            var genreBase = await _dbContext.Genres.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (genreBase == null)
            {
                return BadRequest("Genre doesn't exist");
            }
            var genre = new GenreModel(genreBase.Id, genreBase.Name);
            return Ok(genre);
        }
        [HttpPost("add-genre")]
        public async Task<ActionResult> AddGenre([FromBody] GenreModel genreModel)
        {
   
            var genre = new Genre { Name = genreModel.Name };
            
            _dbContext.Genres.Add(genre);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("update-genre")]
        public async Task<ActionResult<GenreModel>> UpdateGenre([FromBody] GenreModel genreModel)
        {
            var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id ==  genreModel.Id);
            if (genre == null)
            {
                return BadRequest("Genre doesn't exist");
            }

            genre.Name = genreModel.Name;

            await _dbContext.SaveChangesAsync();

            return Ok(genreModel);
      
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var genre = await _dbContext.Genres.FirstAsync(x => x.Id == id);
            if (genre == null)
            {
                return BadRequest("Genre doesn't exist!");
            }

            
            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync();
            return Ok();
           
        }
    }
}
