using Task1.Context;
using Task1.DAL.IRepositories;
using Task1.Models;

namespace Task1.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
