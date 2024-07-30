using Gerenciamento.Application.Constants;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Gerenciamento.Domain.Services;

namespace Gerenciamento.Application
{
    public class ProjectService : IProjectService
    {
        private readonly IDbProjectReadAdapter _dbProjetoReadAdapter;
        private readonly IDbProjectWriteAdapter _dbProjetoWriteAdapter;
        public ProjectService(IDbProjectReadAdapter dbProjetoReadAdapter, IDbProjectWriteAdapter dbProjetoWriteAdapter)
        {
            this._dbProjetoReadAdapter = dbProjetoReadAdapter ??
                 throw new ArgumentNullException(nameof(dbProjetoReadAdapter));
            this._dbProjetoWriteAdapter = dbProjetoWriteAdapter ??
                 throw new ArgumentNullException(nameof(dbProjetoWriteAdapter));
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await _dbProjetoReadAdapter.GetByIdAsync(id);

            if (project == null)
            {
                throw new CustomException(ExceptionMessages.REGISTER_NOT_FOUND);
            }

            bool hasPendingTasks = project.Tasks.Any(t => t.Status == StatusBase.Pending);

            if (hasPendingTasks)
            {
                throw new CustomException(ExceptionMessages.PROJECT_WITH_PENDING_TASK);
            }

            await _dbProjetoWriteAdapter.DeleteAsync(project);
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _dbProjetoReadAdapter.GetAllAsync();
        }

        public async Task SaveProjectAsync(Project project)
        {
            if (project is null)
                throw new CustomException(ExceptionMessages.REGISTER_IS_EMPTY);

            await _dbProjetoWriteAdapter.SaveAsync(project);
        }
    }
}
