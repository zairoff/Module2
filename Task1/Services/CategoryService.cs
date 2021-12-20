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
    public class CategoryService : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("category not found");

            _unitOfWork.Category.Remove(category);
            await _unitOfWork.CompleteAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _unitOfWork.Category.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("category not found");

            return category;
        }

        public async Task<Category> UpdateAsync(Category entity)
        {
            _unitOfWork.Category.Update(entity);
            await _unitOfWork.CompleteAsync();

            return entity;
        }
    }
}
