using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task1.Context;
using Task1.DAL.Repositories;
using Task1.DAL.UnitOfWork;
using Task1.Models;
using Xunit;

namespace Task1Test.DAL
{
    public class CategoryRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> options;
        private readonly Category category = new() { CategoryID = 1, CategoryName = "Test1", Description = "Test1" };

        public CategoryRepositoryTests()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: "temp.db").Options;
        }

        [Fact]        
        public async Task Add_Should_Add_Category()
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
                var result = await context.Categories.FirstOrDefaultAsync();

                Assert.NotNull(result);
                Assert.Equal(category.CategoryID, result.CategoryID);
                Assert.Equal(category.CategoryName, result.CategoryName);
            }                
        }

        [Fact]
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

                Assert.NotNull(result);
                Assert.Equal(expectedResult.CategoryName, result.CategoryName);
            }
        }

        [Fact]
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

                Assert.Null(result);
            }
        }

        [Fact]
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

                Assert.NotNull(result);
            }
        }
    }
}