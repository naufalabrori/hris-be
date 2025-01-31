using HRIS.Core.Interfaces.Repositories;
using HRIS.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Data.Repositories
{
    public class HrisRepository : IHrisRepository
    {
        private readonly HrisContext _hrisContext;

        public HrisRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _hrisContext.SaveChangesAsync(cancellationToken);
        }
    }
}
