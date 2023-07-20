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
        public DbSet<EarliestProjectionPerCinema> EarliestProjectionsPerCinema { get; set; }
        public MoviesDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add the configuration for the view mapping here
            modelBuilder
                .Entity<EarliestProjectionPerCinema>()
                .ToView(nameof(EarliestProjectionPerCinema))
                .HasKey(t => t.CinemaId);

            // Other entity configurations, if any
        }

    }
}
