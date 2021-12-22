using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Services.Contracts;
using Task1.ViewModels;

namespace Task1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ISupplierService supplierService, ICategoryService categoryService)
        {
            _productService = productService;
            _supplierService = supplierService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var productViewModel = new ProductViewModel
            {
                Product = await _productService.GetByIdAsync(id),
                Categories = await _categoryService.GetAllAsync(),
                Suppliers = await _supplierService.GetAllAsync()
            };

            return View(productViewModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost()
        {
            if (ModelState.IsValid)
                return Content(ModelState.Values.ToString());

            return Content(id.ToString());
        }
    }
}
