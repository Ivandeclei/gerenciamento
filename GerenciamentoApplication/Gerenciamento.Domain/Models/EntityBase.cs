namespace Gerenciamento.Domain.Models
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
