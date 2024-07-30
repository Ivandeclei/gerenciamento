namespace Gerenciamento.Domain.Models
{
    public class Comments : EntityBase
    {
        public Guid TaskProjectId { get; set; }
        public string? Comment { get; set; }
        public TaskProject? TaskProject { get; set; }
    }
}
