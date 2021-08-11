using System.Threading;
using System.Threading.Tasks;

namespace DDDTemplate.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChanges(CancellationToken cancellationToken = default);
    }
}
