using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Services.Contracts;

namespace Task1.Services
{
    public class ImageFileSystemService : IFileService
    {
        private readonly IConfiguration _configuration;

        public ImageFileSystemService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> SaveFileAsync(byte[] file)
        {
            var filePath = Path.Combine(_configuration["Cache:Path"], Path.GetRandomFileName());
            await File.WriteAllBytesAsync(filePath, file);

            return filePath;
        }

        public async Task<byte[]> GetFileAsync(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }
    }
}
