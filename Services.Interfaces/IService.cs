namespace Services.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<int> Create(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, T entity);
    }
}
