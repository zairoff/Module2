using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Controllers;
using Task1.Models;
using Task1.Services.Contracts;
using Xunit;

namespace Task1Test.Controller
{
    public class CategoryControllerTests
    {
            private Mock<ICategoryService> _mockService;
            private CategoryController _controller;
            private readonly Category category1 = new() { CategoryID = 1, CategoryName = "A", Description = "B" };
            private readonly Category category2 = new() { CategoryID = 2, CategoryName = "C", Description = "D" };

        public CategoryControllerTests()
        {
            _mockService = new Mock<ICategoryService>();
            _controller = new CategoryController(_mockService.Object);
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithListOfCategories()
        {
            var mockedResult = new List<Category> { category1, category2 };
            var expected = new List<Category> { category1, category2 };

            _mockService.Setup(c => c.GetAllAsync()).ReturnsAsync(expected);

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
