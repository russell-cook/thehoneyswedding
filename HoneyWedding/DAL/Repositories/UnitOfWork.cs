using System;

namespace HoneyWedding.DAL.Repositories
{
    public partial class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        //private GenericRepository<modelType> modelRepository;

        // template for GenericRepository implementation:
        //public GenericRepository<modelType> ModelRepository
        //{
        //    get
        //    {

        //        if (this.modelRepository == null)
        //        {
        //            this.modelRepository = new GenericRepository<modelType>(context);
        //        }
        //        return modelRepository;
        //    }
        //}

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