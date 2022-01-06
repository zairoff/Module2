using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Task1.Models;
using Task1.Services.Contracts;
using Task1.ViewModels;

namespace Task1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();

            return View(categories);
        }

        [HttpGet, Route(nameof(Picture))]
        public async Task<IActionResult> Picture(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            var categoryView = CategoryToView(category);

            return View(categoryView);
        }

        [HttpPost, Route(nameof(Picture))]
        public async Task<IActionResult> Picture(CategoryView categoryView)
        {
            var category = await _categoryService.GetByIdAsync(categoryView.CategoryID);

            using (var memoryStream = new MemoryStream())
            {
                await categoryView.FormFile.CopyToAsync(memoryStream);

                category.Picture = memoryStream.ToArray();

                await _categoryService.UpdateAsync(category);
            }

            var resultView = CategoryToView(category);

            return View(resultView);
        }

        private CategoryView CategoryToView(Category category)
        {
            return new CategoryView
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Picture = category.Picture
            };
        }
    }
}
