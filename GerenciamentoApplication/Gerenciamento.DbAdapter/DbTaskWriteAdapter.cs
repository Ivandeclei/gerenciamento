using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter
{
    public class DbTaskWriteAdapter : IDbTaskWriteAdapter
    {
        private readonly Context _context;
        private DbSet<TaskProject> _task;
        public DbTaskWriteAdapter(Context context)
        {
            _context = context;
            _task = _context.Set<TaskProject>();
        }
        public async Task DeleteAsync(TaskProject task)
        {
            _task.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> SaveAsync(TaskProject task)
        {
            _task.AddAsync(task);
            await _context.SaveChangesAsync();
            return task.Id;
        }

        public async Task UpdateAsync(TaskProject task)
        {
            var existingTask = await _task
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(t => t.Id == task.Id);

            if (existingTask != null)
            {
                task.Priority = existingTask.Priority;
                _task.Entry(task).State = EntityState.Modified;
                _task.Entry(task).Property(t => t.Priority).IsModified = false;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Task not found");
            }
        }

    }
}
