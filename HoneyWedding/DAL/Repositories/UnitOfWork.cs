using HoneyWedding.Models;
using System;
using System.Threading.Tasks;

namespace HoneyWedding.DAL.Repositories
{
    public partial class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<WeddingGuest> _weddingGuestRepository;
        private GenericRepository<AccommodationLocation> _accommodationsRepository;

        public GenericRepository<WeddingGuest> WeddingGuestRepository
        {
            get
            {

                if (this._weddingGuestRepository == null)
                {
                    this._weddingGuestRepository = new GenericRepository<WeddingGuest>(context);
                }
                return _weddingGuestRepository;
            }
        }

        public GenericRepository<AccommodationLocation> AccommodationsRepository
        {
            get
            {

                if (this._accommodationsRepository == null)
                {
                    this._accommodationsRepository = new GenericRepository<AccommodationLocation>(context);
                }
                return _accommodationsRepository;
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}