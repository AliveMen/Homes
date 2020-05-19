using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Homes.Infrastructure
{
    public class DbContextUnitOfWork : IUnitOfWork
    {
        public DbContextUnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public DbContext DbContext { get; private set; }
        public int Commit()
        {
            var result = DbContext.SaveChanges();
            return result;
        }

        public Task<int> CommitAsync()
        {
            var result = DbContext.SaveChangesAsync();
            return result;
        }
    }
}