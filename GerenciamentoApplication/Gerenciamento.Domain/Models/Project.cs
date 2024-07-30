namespace Gerenciamento.Domain.Models
{
    public class Project : EntityBase
    {
        public string? Name { get; set; }
        public IEnumerable<TaskProject>? Tasks { get; set; }
    }
}
