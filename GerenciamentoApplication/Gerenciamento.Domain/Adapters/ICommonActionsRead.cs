namespace Gerenciamento.Domain.Adapters
{
    public interface ICommonActionsRead<T>
    {
        Task<T> GetByIdAsync(Guid id);
        
    }
}
