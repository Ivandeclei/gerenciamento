using Gerenciamento.Domain.Models;

namespace GerenciamentoApplication.Dtos
{
    public class TaskUpdate : BasePost
    {
        public TaskDto? Task { get; set; }
    }
}
