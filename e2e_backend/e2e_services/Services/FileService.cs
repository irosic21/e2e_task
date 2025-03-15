using e2e_services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_services.Services
{
    public class FileService : IFileService
    {
        private readonly string _webRootPath;
        private readonly string _baseDirectory;
        private readonly string _subDirectory;

        public FileService(IConfiguration configuration)
        {
            _webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            _subDirectory = configuration["FileStorage:SubDirectory"] ?? "pictures";

        }
        public bool DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_webRootPath, _subDirectory, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }

        public string GetFilePath(string fileName)
        {
            return $"{fileName}"; 
        }

        public async Task<string> SaveFileInDirectoryAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            var directoryPath = Path.Combine(_webRootPath, _subDirectory);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(directoryPath, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return newFileName; 
        }
    }
}
