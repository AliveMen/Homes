namespace Homes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Homes.Extensioons;
    using Homes.Infrastructure;
    using Homes.Interfaces;
    using Homes.Models;

    public class BuildingService : IBuildingService
    {
        private readonly Func<IHomesRepository> _homeRepositoryFactory;

        public BuildingService(Func<IHomesRepository> homeRepositoryFactory)
        {
            _homeRepositoryFactory = homeRepositoryFactory;
        }

        public async Task<Building[]> SearchAsync(SearchCriteria criteria)
        {
            
            using (var repository = _homeRepositoryFactory())
            {
                var query = repository.Buildings;

                if (!criteria.ObjectIds.IsNullOrEmpty())
                {
                    query = query.Where(x => criteria.ObjectIds.Contains(x.Id));
                }
                else
                {
                    if (criteria.Keyword != null)
                    {
                        query = query.Where(x => x.Name.Contains(criteria.Keyword));
                    }
                }

                var buildingIds = query.OrderBy(x => x.Name).ThenBy(x => x.Id).Skip(criteria.Skip).Take(criteria.Take).Select(x => x.Id).ToArray();

                var result = await GetByIdsAsync(buildingIds);
                return result;
            }
        }

        public async Task<Building[]> GetByIdsAsync(string[] ids)
        {
            using (var repository = _homeRepositoryFactory())
            {
                return await repository.GetBuildingsByIdsAsync(ids);
            }

        }

        public async Task DeleteAsync(string[] ids)
        {
            using (var repository = _homeRepositoryFactory())
            {
                var buildings = await repository.GetBuildingsByIdsAsync(ids);

                foreach (var building in buildings)
                {
                    repository.Remove(building);
                }

                await repository.UnitOfWork.CommitAsync();
            }
        }

        public async Task<Building[]> SaveAsync(Building[] buildings)
        {
            var changed = new List<Building>();
            using (var repository = _homeRepositoryFactory())
            {

                var modifiedEntities = buildings.Where(x => !x.IsTransient());
                foreach (var modifiedEntity in modifiedEntities)
                {
                    repository.Update(modifiedEntity);
                    changed.Add(modifiedEntity);
                }


                var newEntities = buildings.Where(x => x.IsTransient());
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