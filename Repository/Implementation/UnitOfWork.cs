using Data.Context;
using Data.Entities;
using Repositories.Implementations;

namespace Repository.Implementation
{
    public class UnitOfWork : IDisposable
    {
        private readonly MovieDBContext context;
        private GenericRepository<Actor> actorsRepository;
        private GenericRepository<Movie> moviesRepository;
        private GenericRepository<Studio> studiosRepository;

        public UnitOfWork()
        {
            context = new MovieDBContext();
        }

        public GenericRepository<Actor> ActorsRepository
        {
            get
            {

                if (actorsRepository == null)
                {
                    actorsRepository = new GenericRepository<Actor>(context);
                }
                return actorsRepository;
            }
        }

        public GenericRepository<Movie> MovieRepository
        {
            get
            {

                if (moviesRepository == null)
                {
                    moviesRepository = new GenericRepository<Movie>(context);
                }
                return moviesRepository;
            }
        }
        public GenericRepository<Studio> StudioRepository
        {
            get
            {

                if (studiosRepository == null)
                {
                    studiosRepository = new GenericRepository<Studio>(context);
                }
                return studiosRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}