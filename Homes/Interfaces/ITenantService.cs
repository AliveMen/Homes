using System.Threading.Tasks;
using Homes.Models;

namespace Homes.Interfaces
{
    public interface ITenantService
    {
        Task<Tenant[]> SearchAsync(TenantSearchCriteria criteria);
        Task<Tenant[]> GetByIdsAsync(string[] ids);
        Task DeleteAsync(string[] id);
        Task<Tenant[]> SaveAsync(Tenant[] tenant);
    }
}