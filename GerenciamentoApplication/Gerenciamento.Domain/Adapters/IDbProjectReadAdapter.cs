using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Adapters
{
    public interface IDbProjectReadAdapter : ICommonActionsRead<Project>
    {
        Task<IEnumerable<Project>> GetAllAsync();
    }
}
