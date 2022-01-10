using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Threading.Tasks;
using Task1.Attributes;
using Task1.Enums;
using Task1.Models;
using Task1.Services.Contracts;
using Task1.ViewModels;

namespace Task1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICacheService _cacheService;

        public CategoryController(ICategoryService categoryService, ICacheService cacheService)
        {
            _categoryService = categoryService;
            _cacheService = cacheService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();

            return View(categories);
        }

        /// <summary>
        /// Return an image from cached in middleware
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category</returns>
        [HttpGet, Route(nameof(Picture))]
        [Cache(CacheBy = CacheType.Image)]
        public async Task<IActionResult> Picture(int id)
        {
            var obj = await _cacheService.GetCacheAsync(id);

            if(obj is byte[] image)
            {
                var category = new Category { CategoryID = id, Picture = image };
                return View(category);
            }

            // test
            return Content("No cache!");
        }

        [HttpGet, Route(nameof(PictureEdit))]
        public async Task<IActionResult> PictureEdit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            var categoryView = CategoryToView(category);

            return View(categoryView);
        }

        [HttpPost, Route(nameof(PictureEdit))]
        public async Task<IActionResult> PictureEdit(CategoryView categoryView)
        {
            var category = await _categoryService.GetByIdAsync(categoryView.CategoryID);

            using (var memoryStream = new MemoryStream())
            {
                await categoryView.FormFile.CopyToAsync(memoryStream);

                category.Picture = memoryStream.ToArray();

                await _categoryService.UpdateAsync(category);
            }

            var resultView = CategoryToView(category);

            return RedirectToAction(nameof(Picture), new { id = resultView.CategoryID });
        }

        [HttpGet, Route("/images/{id}")]
        public async Task<IActionResult> Images(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return View(category);
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
