using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Services
{
    public interface ITaskService
    {
        Task SaveTaskAsync(TaskProject task, User user);
        Task<IEnumerable<TaskProject>> GetTaskAsync(Guid idProject);
        Task UpdateTaskAsync(TaskProject task, User user);
        Task DeleteTaskAsync(Guid id, User user);
        Task SaveCommentAsync(Comments comments, User user);

    }
}
