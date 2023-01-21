namespace WebApplication1.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T Entity);
        Task<T> Update(T Entity);
        T Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
    }
}
