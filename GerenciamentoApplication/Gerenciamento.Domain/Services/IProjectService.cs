using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Services
{
    public interface IProjectService
    {
        Task SaveProjectAsync(Project project);
        Task<IEnumerable<Project>> GetProjectsAsync();

        Task DeleteProjectAsync(Guid id);

    }
}
