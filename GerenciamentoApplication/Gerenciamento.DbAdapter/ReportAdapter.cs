using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter
{
    public class ReportAdapter : IReportAdapter
    {
        private readonly Context _context;
        private DbSet<TaskProject> _task;
        public ReportAdapter(Context context)
        {
            _context = context;
            _task = _context.Set<TaskProject>();
        }
        public async Task<IEnumerable<ReportTask>> GetByIdAsync()
        {
            var completedTasksByUser = await _task
                .Where(t => t.Status == StatusBase.Completed) 
                .GroupBy(t => t.NameUser)
                .Select(g => new ReportTask
                {
                    User = new User { Name = g.Key },
                    NumberOfTask = g.Count()
                })
                .ToListAsync();

            return completedTasksByUser;
        }
    }
}
