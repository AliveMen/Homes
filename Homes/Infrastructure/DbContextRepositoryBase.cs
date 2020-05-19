using Homes.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Homes.Infrastructure
{
    public abstract class DbContextRepositoryBase<TContext> : IRepository where TContext : DbContext
    {
        protected DbContextRepositoryBase(TContext dbContext, IUnitOfWork unitOfWork = null)
        {
            DbContext = dbContext;
            UnitOfWork = unitOfWork;

            UnitOfWork = unitOfWork ?? new DbContextUnitOfWork(dbContext);
        }

        public TContext DbContext { get; private set; }
        public IUnitOfWork UnitOfWork { get; private set; }

        public void Attach<T>(T item) where T : class
        {
            DbContext.Attach(item);
        }

        public void Add<T>(T item) where T : class
        {
            DbContext.Add(item);
        }

        public void Update<T>(T item) where T : class
        {
            DbContext.Update(item);
            DbContext.Entry(item).State = EntityState.Modified;
        }

        public void Remove<T>(T item) where T : class
        {
            DbContext.Remove(item);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && DbContext != null)
            {
                DbContext.Dispose();
                DbContext = null;
                UnitOfWork = null;
            }
        }

    }
}