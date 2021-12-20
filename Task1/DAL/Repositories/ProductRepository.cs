using Task1.Context;
using Task1.Dal.Repositories;
using Task1.DAL.IRepositories;
using Task1.Models;

namespace Task1.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
