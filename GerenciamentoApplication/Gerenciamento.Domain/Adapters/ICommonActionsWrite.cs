namespace Gerenciamento.Domain.Adapters
{
    public interface ICommonActionsWrite<T>
    {
        Task<Guid> SaveAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
