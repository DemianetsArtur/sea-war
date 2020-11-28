using System.Threading;
using System.Threading.Tasks;

namespace SeaWar.API.Infrastructure.Interfaces
{
    public interface IHubService
    {
        Task HubWork(CancellationToken cancellationToken);
    }
}
