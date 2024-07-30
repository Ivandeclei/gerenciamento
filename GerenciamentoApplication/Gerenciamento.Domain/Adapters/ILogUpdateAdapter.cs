using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Adapters
{
    public interface ILogUpdateAdapter
    {
        Task SaveAsync(HistoryUpdate historyUpdate);
    }
}
