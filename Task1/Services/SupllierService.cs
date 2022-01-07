using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.DAL.IRepositories;
using Task1.DAL.UnitOfWork;
using Task1.Exceptions;
using Task1.Models;
using Task1.Services.Contracts;

namespace Task1.Services
{
    public class SupllierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupllierService(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository)
        {
            _unitOfWork = unitOfWork;
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier> AddAsync(Supplier entity)
        {
            await _supplierRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }

        public async Task<Supplier> DeleteAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);

            if (supplier == null)
                throw new NotFoundException("supplier not found");

            _supplierRepository.Remove(supplier);
            await _unitOfWork.CompleteAsync();

            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);

            if (supplier == null)
                throw new NotFoundException("supplier not found");

            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Supplier entity)
        {
            _supplierRepository.Update(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }
    }
}
