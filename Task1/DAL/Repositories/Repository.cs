using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Context;
using Task1.DAL.IRepositories;

namespace Task1.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> DbSet => _dbSet ??= _context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
