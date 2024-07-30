using Gerenciamento.Domain.Models;

namespace GerenciamentoApplication.Dtos
{
    public class TaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public StatusBase Status { get; set; }
        public Guid? ProjectId { get; set; }

    }
}
