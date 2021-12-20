using System.Threading.Tasks;
using Task1.Context;
using Task1.DAL.IRepositories;
using Task1.DAL.Repositories;

namespace Task1.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
        }

        public ICategoryRepository Category { get; private set; }

        public ISupplierRepository Supplier { get; private set; }

        public IProductRepository Product { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
