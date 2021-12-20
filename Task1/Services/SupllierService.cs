using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.DAL.UnitOfWork;
using Task1.Exceptions;
using Task1.Models;
using Task1.Services.Contracts;

namespace Task1.Services
{
    public class SupllierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupllierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Supplier> DeleteAsync(int id)
        {
            var supplier = await _unitOfWork.Supplier.GetByIdAsync(id);

            if (supplier == null)
                throw new NotFoundException("supplier not found");

            _unitOfWork.Supplier.Remove(supplier);
            await _unitOfWork.CompleteAsync();

            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _unitOfWork.Supplier.GetAllAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            var supplier = await _unitOfWork.Supplier.GetByIdAsync(id);

            if (supplier == null)
                throw new NotFoundException("supplier not found");

            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Supplier entity)
        {
            _unitOfWork.Supplier.Update(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }
    }
}
