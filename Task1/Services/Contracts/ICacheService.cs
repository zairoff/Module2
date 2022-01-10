using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1.Services.Contracts
{
    public interface ICacheService
    {
        Task CacheAsync(object key);

        Task<object> GetCacheAsync(object key);
    }
}