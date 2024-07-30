using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;

namespace Gerenciamento.DbAdapter
{
    public class UserDbAdapter : IUserDbAdapter
    {
        public Task<IEnumerable<User>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
