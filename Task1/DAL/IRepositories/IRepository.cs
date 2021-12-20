using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task1.DAL.IRepositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
