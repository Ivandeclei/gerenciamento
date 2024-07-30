using Gerenciamento.Domain.Models;

namespace GerenciamentoApplication.Dtos
{
    public class UserDto
    {
        public string? Name { get; set; }
        public TypeUser TypeUser { get; set; }
    }
}
