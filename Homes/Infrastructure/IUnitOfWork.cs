using System.Threading.Tasks;

namespace Homes.Infrastructure
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
