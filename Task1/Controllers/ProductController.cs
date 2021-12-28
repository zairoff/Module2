using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Models;
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

        [HttpGet, Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var product = id == null ? new Product() : await _productService.GetByIdAsync((int)id);
            var categories = await _categoryService.GetAllAsync();
            var suppliers = await _supplierService.GetAllAsync();

            var productViewModel = new ProductUpdateViewModel
            {
                Product = ProductToView(product),
                Categories = CategoryToView(categories),
                Suppliers = SupplierToView(suppliers)
            };

            ViewBag.Title = id == null ? "New Product" : "Update Product";

            return View(productViewModel);
        }

        [HttpPost, Route("Edit")]
        public async Task<IActionResult> Edit(ProductView productViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Update Product";
                return View();
            }                

            var product = ViewToProduct(productUpdateViewModel.Product);
            if (product.ProductID == 0)
                await _productService.AddAsync(product);
            else
                await _productService.UpdateAsync(product);

            return RedirectToAction(nameof(Index));
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
                ProductID = productView.ProductID,
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
