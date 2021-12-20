using Task1.Context;
using Task1.Dal.Repositories;
using Task1.DAL.IRepositories;
using Task1.Models;

namespace Task1.DAL.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context)
        {
        }
    }
}
