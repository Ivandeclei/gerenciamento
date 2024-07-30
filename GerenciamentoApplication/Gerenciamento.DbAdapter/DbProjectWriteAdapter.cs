using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter
{
    public class DbProjectWriteAdapter : IDbProjectWriteAdapter
    {
        private readonly Context _context;
        private DbSet<Project> _projeto;
        public DbProjectWriteAdapter(Context context)
        {
            _context = context;
            _projeto = _context.Set<Project>();
        }

        public async Task DeleteAsync(Project project)
        {
            _projeto.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> SaveAsync(Project project)
        {
            _projeto.AddAsync(project);
            await _context.SaveChangesAsync();
            return project.Id;
        }

        public async Task UpdateAsync(Project project)
        {
            _projeto.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
