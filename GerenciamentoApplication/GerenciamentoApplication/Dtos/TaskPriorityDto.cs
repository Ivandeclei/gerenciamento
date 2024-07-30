using Gerenciamento.Domain.Models;

namespace GerenciamentoApplication.Dtos
{
    public class TaskPriorityDto : TaskUpdate
    {
        public Priority Priority { get; set; }
    }
}
