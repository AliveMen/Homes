namespace Homes.Interfaces
{
    using System.Threading.Tasks;

    using Homes.Models;

    public interface IBuildingService
    {
        Task<Building[]> SearchAsync(SearchCriteria criteria);

        Task<Building[]> GetByIdsAsync(string[] ids);

        Task DeleteAsync(string[] ids);

        Task<Building[]> SaveAsync(Building[] building);
    }
}