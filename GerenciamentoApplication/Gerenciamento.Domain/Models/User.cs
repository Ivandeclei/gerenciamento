namespace Gerenciamento.Domain.Models
{
    public class User : EntityBase
    {
        public string? Name { get; set; }
        public TypeUser TypeUser { get; set; }
    }
}
