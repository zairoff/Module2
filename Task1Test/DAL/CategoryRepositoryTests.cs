using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Task1.Context;
using Task1.DAL.Repositories;
using Task1.DAL.UnitOfWork;
using Task1.Models;

namespace Task1Test.DAL
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private DbContextOptions<AppDbContext> options;
        private readonly Category category = new() { CategoryID = 1, CategoryName = "Test1", Description = "Test1" };

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(databaseName: "temp.db").Options;
        }

        [Test]
        public async Task Add_Should_Add_Category()
        {
            using (var context = new AppDbContext(options))
            {
                var repository = new CategoryRepository(context);
                var unitofwork = new UnitOfWork(context);

                await repository.AddAsync(category);
                await unitofwork.CompleteAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var result = await context.Categories.FirstOrDefaultAsync();

                Assert.IsNotNull(result);
                Assert.AreEqual(category.CategoryID, result.CategoryID);
                Assert.AreEqual(category.CategoryName, result.CategoryName);
            }                
        }

        [Test]
        public async Task Update_Should_Update_Category()
        {
            var expectedResult = new Category { CategoryID = 2, CategoryName = "AA" };
            
            using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();

                var repository = new CategoryRepository(context);
                var unitofwork = new UnitOfWork(context);

                await repository.AddAsync(category);
                await unitofwork.CompleteAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var entity = await context.Categories.FirstOrDefaultAsync();
                entity.CategoryName = "AA";

                var repository = new CategoryRepository(context);
                var unitofwork = new UnitOfWork(context);

                repository.Update(entity);
                await unitofwork.CompleteAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var result = await context.Categories.FirstOrDefaultAsync();

                Assert.IsNotNull(result);
                Assert.AreEqual(expectedResult.CategoryName, result.CategoryName);
            }
        }

        [Test]
        public async Task Delete_Should_Delete_Category()
        {
            using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();

                var repository = new CategoryRepository(context);
                var unitofwork = new UnitOfWork(context);

                await repository.AddAsync(category);
                await unitofwork.CompleteAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var entity = await context.Categories.FirstOrDefaultAsync();
                var repository = new CategoryRepository(context);
                var unitofwork = new UnitOfWork(context);

                repository.Remove(entity);
                await unitofwork.CompleteAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var result = await context.Categories.FirstOrDefaultAsync();

                Assert.IsNull(result);
            }
        }

        [Test]
        public async Task GetByID_Should_Get_Category_ByID()
        {
            using (var context = new AppDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();

                var repository = new CategoryRepository(context);
                var unitofwork = new UnitOfWork(context);

                await repository.AddAsync(category);
                await unitofwork.CompleteAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new CategoryRepository(context);
                var result = await repository.GetByIdAsync(category.CategoryID);

                Assert.IsNotNull(result);
            }
        }
    }
}