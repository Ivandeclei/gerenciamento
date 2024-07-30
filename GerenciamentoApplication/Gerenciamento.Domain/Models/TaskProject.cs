namespace Gerenciamento.Domain.Models
{
    public class TaskProject : EntityBase
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public StatusBase Status { get; set; }
        public Priority Priority { get; set; }
        public IEnumerable<HistoryUpdate>? Histories{ get; set; }
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }
        public string? NameUser { get; set; }
    }
}
