using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Services
{
    public interface IUserService
    {
        Task SaveUserAsync(User user);
        Task<IEnumerable<User>> GetUsuarios();
    }
}
