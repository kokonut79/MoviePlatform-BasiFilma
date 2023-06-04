using Data.Entities;
using System.Data.Entity;

namespace Data.Context
{
    public class MovieDBContext : DbContext
    {
         public DbSet<Actor>Actors { get; set; }
         public DbSet<Movie> Movies { get; set; }
         public DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .HasRequired(a => a.Movie)
                .WithMany(m => m.Actors)
                .HasForeignKey(a => a.MovieId)
                .WillCascadeOnDelete(false);
        }
    }
}
