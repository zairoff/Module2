using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Context;
using Task1.DAL.IRepositories;
using Task1.Models;

namespace Task1.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Supplier).Include(p => p.Category).ToListAsync();
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                .Include(p => p.Supplier)
                                .Include(p => p.Category)
                                .Where(p => p.ProductID == id)
                                .FirstOrDefaultAsync();
        }
    }
}
