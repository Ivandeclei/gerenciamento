using Gerenciamento.Domain.Models;

namespace GerenciamentoApplication.Dtos
{
    public class TaskPost :UserDto
    {
        public Priority Priority { get; set; }
        public TaskDto? Task { get; set; }
    }
}
