using Homes.Models;
using Homes.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Homes.Infrastructure
{
    public interface IHomesRepository : IRepository
    {
        IQueryable<Building> Buildings { get; }
        IQueryable<Tenant> Tenants { get; }

        Task<Building[]> GetBuildingsByIdsAsync(string[] buildingIds);

        Task<Tenant[]> GetTenantsByIdsAsync(string[] tenantsIds);

    }
}