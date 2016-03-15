﻿using HoneyWedding.Models;
using System;

namespace HoneyWedding.DAL.Repositories
{
    public partial class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<WeddingGuest> _weddingGuestRepository;

        // template for GenericRepository implementation:
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