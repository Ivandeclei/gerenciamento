using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Adapters
{
    public interface IDbTaskReadAdapter : ICommonActionsRead<TaskProject>
    {
        Task<IEnumerable<TaskProject>> GetAllTaskByProjectAsync( Guid idProject);
        Task<int> GetCountTaskByIdProjectAsync(Guid idProject);

    }
}
