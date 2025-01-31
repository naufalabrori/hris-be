using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Core.Interfaces.Repositories
{
    public interface IHrisRepository
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
