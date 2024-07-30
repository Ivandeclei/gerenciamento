using Gerenciamento.Application.Constants;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Gerenciamento.Domain.Services;

namespace Gerenciamento.Application
{
    public class ReportService : IReportService
    {
        private readonly IReportAdapter _reportAdapter;
        public ReportService(IReportAdapter reportAdapter)
        {
            this._reportAdapter = reportAdapter ??
                 throw new ArgumentNullException(nameof(reportAdapter));
        }
        public async Task<IEnumerable<ReportTask>> GetTaskByUserAsync(User user)
        {
            UserIsValid(user);
            return await _reportAdapter.GetByIdAsync();
        }

        private void UserIsValid(User user)
        {
            if (user == null)
                throw new CustomException(ExceptionMessages.REGISTER_IS_EMPTY);
            if(user.TypeUser != TypeUser.Manager)
                throw new CustomException(ExceptionMessages.USER_NOT_AUTHORIZED);
        }
    }
}
