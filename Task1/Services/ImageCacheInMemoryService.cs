using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Services.Contracts;

namespace Task1.Services
{
    public class ImageCacheInMemoryService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;

        public ImageCacheInMemoryService(IMemoryCache memoryCache, IFileService fileService, ICategoryService categoryService)
        {
            _memoryCache = memoryCache;
            _fileService = fileService;
            _categoryService = categoryService;
        }

        public async Task CacheAsync(object key)
        {
            if (key is not int)
                throw new InvalidOperationException();

            int id = (int)key;
            if (!_memoryCache.TryGetValue(id, out string _))
            {
                var category = await _categoryService.GetByIdAsync(id);
                string path = await _fileService.SaveFileAsync(category.Picture);
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(500),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(200)
                };

                _memoryCache.Set(id, path, cacheExpiryOptions);
            }
        }

        public async Task<object> GetCacheAsync(object key)
        {
            if (key is not int)
                throw new InvalidOperationException();

            if (!_memoryCache.TryGetValue((int)key, out string path))
                throw new KeyNotFoundException();

            return await _fileService.GetFileAsync(path);
        } 
    }
}
