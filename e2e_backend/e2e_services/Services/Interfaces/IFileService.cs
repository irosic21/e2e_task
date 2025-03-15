using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_services.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileInDirectoryAsync(IFormFile file);
        bool DeleteFile(string fileName);
        string GetFilePath(string fileName);
    }
}
