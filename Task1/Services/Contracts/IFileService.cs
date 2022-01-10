using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1.Services.Contracts
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(byte[] file);
        Task<byte[]> GetFileAsync(string path);
    }
}
