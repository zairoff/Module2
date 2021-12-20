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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException("product not found");

            _unitOfWork.Product.Remove(product);
            await _unitOfWork.CompleteAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.Product.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException("product not found");

            return product;
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            _unitOfWork.Product.Update(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }
    }
}
