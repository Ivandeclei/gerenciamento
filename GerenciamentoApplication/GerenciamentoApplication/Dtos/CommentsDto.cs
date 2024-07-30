namespace GerenciamentoApplication.Dtos
{
    public class CommentsDto : UserDto
    {
        public Guid? TaskProjectId { get; set; }
        public string? Comment { get; set; }
    }
}
