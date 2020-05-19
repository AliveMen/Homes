using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homes.Extensioons;
using Homes.Infrastructure;
using Homes.Interfaces;
using Homes.Models;
using Microsoft.EntityFrameworkCore;

namespace Homes.Services
{
    public class TenantService : ITenantService
    {
        private readonly Func<IHomesRepository> _homeRepositoryFactory;

        public TenantService(Func<IHomesRepository> homeRepositoryFactory)
        {
            _homeRepositoryFactory = homeRepositoryFactory;
        }

        public async Task<Tenant[]> SearchAsync(TenantSearchCriteria criteria)
        {
            using (var repository = _homeRepositoryFactory())
            {
                var query = repository.Tenants;

                if (!criteria.ObjectIds.IsNullOrEmpty())
                {
                    query = query.Where(x => criteria.ObjectIds.Contains(x.Id));
                }
                else
                {
                    if (criteria.BuildingId != null)
                    {
                        query = query.Where(x => x.BuildingId == criteria.BuildingId);
                    }

                    if (criteria.Keyword != null)
                    {
                        query = query.Where(x => x.Name.Contains(criteria.Keyword));
                    }
                }

                var tenantIds = query
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Id)
                    .Skip(criteria.Skip)
                    .Take(criteria.Take)
                    .Select(x => x.Id)
                    .ToArray();
                    
                var result = await GetByIdsAsync(tenantIds);
                return result;
            }
        }

        public async Task<Tenant[]> GetByIdsAsync(string[] ids)
        {
            using (var repository = _homeRepositoryFactory())
            {
                return await repository.Tenants.Where(x => ids.Contains(x.Id)).ToArrayAsync();
            }
        }

        public async Task DeleteAsync(string[] ids)
        {
            var items = await GetByIdsAsync(ids);
            using (var repository = _homeRepositoryFactory())
            {
                foreach (var tenant in items)
                {
                    repository.Remove(tenant);
                }

                await repository.UnitOfWork.CommitAsync();
            }
        }

        public async Task<Tenant[]> SaveAsync(Tenant[] tenants)
        {
            var changed = new List<Tenant>();
            using (var repository = _homeRepositoryFactory())
            {
                var modifiedEntities = tenants.Where(x => !x.IsTransient());
                foreach (var modifiedEntity in modifiedEntities)
                {
                    repository.Update(modifiedEntity);
                    changed.Add(modifiedEntity);
                }

                var newEntities = tenants.Where(x => x.IsTransient());
                foreach (var newEntity in newEntities)
                {
                    repository.Add(newEntity);
                    changed.Add(newEntity);
                }

                await repository.UnitOfWork.CommitAsync();
            }

            return changed.ToArray();
        }
    }
}