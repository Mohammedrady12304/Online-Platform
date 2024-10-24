namespace E_Learning.Repository.IReposatories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T old , T New);
        Task DeleteAsync(string id);
    }

}
