using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Adapters
{
    public interface IUserDbAdapter
    {
        Task SaveAsync(User user);
        Task<IEnumerable<User>> GetAsync();
    }
}
