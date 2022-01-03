using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.DAL.IRepositories;
using Task1.Models;
using Task1.Services;
using Task1.Services.Contracts;
using Xunit;

namespace Task1Test.Service
{
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _mockRepo;
        private ICategoryService _service;
        private readonly Category category1 = new() { CategoryID = 1, CategoryName = "A", Description = "B" };
        private readonly Category category2 = new() { CategoryID = 2, CategoryName = "C", Description = "D" };

        public CategoryServiceTests()
        {
            _mockRepo = new Mock<ICategoryRepository>();
            _service = new CategoryService(/*unitofwork*/ null, _mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_Should_Return_All_Categories()
        {
            var entities = new List<Category> { category1, category2 };
            var expectedResult = new List<Category> { category1, category2 };

            _mockRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(entities);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(result.ToList().Count, expectedResult.Count);
            Assert.Equal(result.ToList()[0].CategoryName, expectedResult[0].CategoryName);
        }
    }
}
