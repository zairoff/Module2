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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return entity;
        }

        public async Task<Product> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException("product not found");

            _productRepository.Remove(product);
            await _unitOfWork.CompleteAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException("product not found");

            return product;
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            _productRepository.Update(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }
    }
}
