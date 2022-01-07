using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.DAL.IRepositories;

namespace Task1.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}
