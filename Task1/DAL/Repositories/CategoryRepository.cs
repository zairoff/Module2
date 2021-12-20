using Task1.Context;
using Task1.Dal.IRepositories;
using Task1.Models;

namespace Task1.Dal.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
