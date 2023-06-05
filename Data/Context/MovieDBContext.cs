using Data.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Data.Context
{
    public class MovieDBContext : DbContext
    {
         public DbSet<Actor>Actors { get; set; }
         public DbSet<Movie> Movies { get; set; }
         public DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>();
            modelBuilder.Entity<Studio>();
            modelBuilder.Entity<Movie>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
