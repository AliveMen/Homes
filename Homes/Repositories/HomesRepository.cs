using System.Linq;
using System.Threading.Tasks;

using Homes.Infrastructure;
using Homes.Models;

namespace Homes.Repositories
{
    using Microsoft.EntityFrameworkCore;

    public class HomesRepository : DbContextRepositoryBase<HomesDbContext>, IHomesRepository
    {
        public HomesRepository(HomesDbContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<Building> Buildings => DbContext.Set<Building>();

        public IQueryable<Tenant> Tenants => DbContext.Set<Tenant>();

        public async Task<Building[]> GetBuildingsByIdsAsync(string[] buildingIds)
        {
            return await Buildings.Where(x => buildingIds.Contains(x.Id)).Include(x => x.Tenants).ToArrayAsync();
        }

        public async Task<Tenant[]> GetTenantsByIdsAsync(string[] tenantsIds)
        {
            return await Tenants.Where(x => tenantsIds.Contains(x.Id)).ToArrayAsync();
        }
    }
}