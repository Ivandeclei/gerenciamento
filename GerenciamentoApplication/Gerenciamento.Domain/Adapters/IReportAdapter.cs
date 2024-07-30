using Gerenciamento.Domain.Models;

namespace Gerenciamento.Domain.Adapters
{
    public interface IReportAdapter
    {
        Task<IEnumerable<ReportTask>> GetByIdAsync();
    }
}
