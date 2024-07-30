using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter
{
    public class DbTaskReadAdapter : IDbTaskReadAdapter
    {
        private readonly Context _context;
        private DbSet<TaskProject> _task;
        public DbTaskReadAdapter(Context context)
        {
            _context = context;
            _task = _context.Set<TaskProject>();
        }

        public async Task<IEnumerable<TaskProject>> GetAllTaskByProjectAsync(Guid idProject)
        {
            return await _task.Include(x => x.Histories)
                .Where(x => x.Project.Id == idProject)
                               .ToListAsync();
        }

        public async Task<TaskProject> GetByIdAsync(Guid id)
        {
            return await _task.Include(x => x.Project)
                .Include(x => x.Histories)
                .Where(x => x.ProjectId == id)
                               .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCountTaskByIdProjectAsync(Guid IdProject)
        {
            return await _task.CountAsync(x => x.Project.Id == IdProject);
        }
    }
}
