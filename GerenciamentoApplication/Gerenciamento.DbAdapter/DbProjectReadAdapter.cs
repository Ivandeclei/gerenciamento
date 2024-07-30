using Gerenciamento.DbAdapter.DbAdapterConfiguration;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento.DbAdapter
{
    public class DbProjectReadAdapter : IDbProjectReadAdapter
    {
        private readonly Context _context;
        private DbSet<Project> _projeto;
        public DbProjectReadAdapter(Context context)
        {
            _context = context;
            _projeto = _context.Set<Project>();
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
                var teste = await _projeto.Include(x => x.Tasks).ToListAsync();

                await _context.SaveChangesAsync();
                return teste;
        }


        public async Task<Project> GetByIdAsync(Guid id)
        {
            return await _projeto
                .Include(s => s.Tasks)
                               .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
