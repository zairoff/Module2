using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Models;
using Task1.Services.Contracts;
using Task1.ViewModels;
using Task1.ViewModels.Product;

namespace Task1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;

        public ProductController(IProductService productService, ISupplierService supplierService, ICategoryService categoryService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _supplierService = supplierService;
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();

            _logger.LogInformation("Product page visited");
            return View(products);
        }        

        [HttpGet, Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Title = "Update Product";
            await PopulateDropdownList();
            var product = await _productService.GetByIdAsync((int)id);
            var productView = ProductToView(product);

            return View(productView);
        }

        [HttpPost, Route("Edit")]
        public async Task<IActionResult> Edit(ProductView productView)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Update Product";
                await PopulateDropdownList();
                return View(productView);
            }                

            var product = ViewToProduct(productView);
           
            await _productService.UpdateAsync(product);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, Route("Create")]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownList();
            ViewBag.Title = "New Product";

            return View();
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(ProductView productView)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "New Product";
                await PopulateDropdownList();
                return View(productView);
            }

            var product = ViewToProduct(productView);

            await _productService.AddAsync(product);

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownList()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Suppliers = await _supplierService.GetAllAsync();
        }

        // Temporary
        private ProductView ProductToView(Product product)
        {
            return new ProductView
            {
                ProductID = product.ProductID,
                CategoryID = product.CategoryID,
                SupplierID = product.SupplierID,
                Discontinued = product.Discontinued,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                ReorderLevel = product.ReorderLevel,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder
            };
        }

        private Product ViewToProduct(ProductView productView)
        {
            return new Product
            {
                ProductID = (productView.ProductID == null) ? 0 : (int)productView.ProductID,
                CategoryID = productView.CategoryID,
                SupplierID = productView.SupplierID,
                Discontinued = productView.Discontinued,
                ProductName = productView.ProductName,
                QuantityPerUnit = productView.QuantityPerUnit,
                ReorderLevel = productView.ReorderLevel,
                UnitPrice = productView.UnitPrice,
                UnitsInStock = productView.UnitsInStock,
                UnitsOnOrder = productView.UnitsOnOrder
            };
        }

        private IEnumerable<CategoryView> CategoryToView(IEnumerable<Category> categories)
        {
            var categoryViews = new List<CategoryView>();

            foreach (var category in categories)
            {
                var categoryView = new CategoryView
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Picture = category.Picture
                };

                categoryViews.Add(categoryView);
            }

            return categoryViews;
        }

        private IEnumerable<SupplierView> SupplierToView(IEnumerable<Supplier> suppliers)
        {
            var supplierViews = new List<SupplierView>();

            foreach (var supplier in suppliers)
            {
                var supplierView = new SupplierView
                {
                    SupplierID = supplier.SupplierID,
                    Address = supplier.Address,
                    City = supplier.City,
                    CompanyName = supplier.CompanyName,
                    ContactName = supplier.ContactName,
                    ContactTitle = supplier.ContactTitle,
                    Country = supplier.Country,
                    Fax = supplier.Fax,
                    HomePage = supplier.HomePage,
                    Phone = supplier.Phone,
                    PostalCode = supplier.PostalCode,
                    Region = supplier.Region
                };

                supplierViews.Add(supplierView);
            }

            return supplierViews;
        }
    }
}
