using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Services
{
    public interface IReportService
    {
        Task<IEnumerable<ReportTask>> GetTaskByUserAsync(User user);
    }
}
