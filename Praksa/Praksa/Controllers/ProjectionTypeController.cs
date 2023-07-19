using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praksa.Models;

namespace Praksa.Controllers
{
    [Route("api/projectiontypes")]
    [ApiController]
    public class ProjectionTypeController : ControllerBase
    {
        private readonly MoviesDbContext _dbContext;
        public ProjectionTypeController(MoviesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProjectionTypeModel>>> GetProjectionTypes()
        {
            var ptypes = await _dbContext.ProjectionTypes.Select(x => new ProjectionTypeModel(x.Id, x.Name)).ToListAsync();

            return Ok(ptypes);
        }
        [HttpPost("add-projection-type")]
        public async Task<ActionResult> AddProjectionType([FromBody] ProjectionTypeModel projectionTypeModel)
        {

            var ptype = new ProjectionType { Name = projectionTypeModel.Name };

            _dbContext.ProjectionTypes.Add(ptype);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteProjectionType(int id)
        {
            var ptype = await _dbContext.ProjectionTypes.FirstAsync(x => x.Id == id);
            if (ptype == null)
            {
                return BadRequest("pType doesn't exist!");
            }


            _dbContext.ProjectionTypes.Remove(ptype);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
