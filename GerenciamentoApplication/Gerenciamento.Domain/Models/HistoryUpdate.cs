namespace Gerenciamento.Domain.Models
{
    public class HistoryUpdate 
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public string? Content { get; set; }
        public string? User { get; set; }
        public string? Action { get; set; }
        public Guid TaskProjectId { get; set; }
        public TaskProject? TaskProject { get; set; }
    }
}
