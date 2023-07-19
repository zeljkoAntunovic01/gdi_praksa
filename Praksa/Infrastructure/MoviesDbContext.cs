using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure
{
    public class MoviesDbContext : DbContext
    {
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<ProjectionType> ProjectionTypes { get; set; }
        public MoviesDbContext(DbContextOptions options) : base(options) { }

    }
}
