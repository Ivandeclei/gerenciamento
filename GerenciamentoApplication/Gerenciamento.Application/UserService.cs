using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Gerenciamento.Domain.Services;

namespace Gerenciamento.Application
{
    public class UserService : IUserService
    {
        private readonly IUserDbAdapter _userDbAdapter;
        public UserService(IUserDbAdapter userDbAdapter)
        {
            this._userDbAdapter = userDbAdapter ??
                throw new ArgumentNullException(nameof(userDbAdapter));
        }
        public async Task<IEnumerable<User>> GetUsuarios()
        {
            return await _userDbAdapter.GetAsync();
        }

        public async Task SaveUserAsync(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            await _userDbAdapter.SaveAsync(user);
        }
    }
}
