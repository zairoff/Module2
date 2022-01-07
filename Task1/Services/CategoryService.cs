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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            await _categoryRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("category not found");

            _categoryRepository.Remove(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("category not found");

            return category;
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            _categoryRepository.Update(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }
    }
}
