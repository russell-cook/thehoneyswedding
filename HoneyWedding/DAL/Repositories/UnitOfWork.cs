using HoneyWedding.Models;
using System;

namespace HoneyWedding.DAL.Repositories
{
    public partial class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Guest> _guestRepository;

        // template for GenericRepository implementation:
        public GenericRepository<Guest> GuestRepository
        {
            get
            {

                if (this._guestRepository == null)
                {
                    this._guestRepository = new GenericRepository<Guest>(context);
                }
                return _guestRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
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