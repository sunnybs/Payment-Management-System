using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Payment_Management_System.Services
{
    public interface IFileManager
    {
        bool VerifyFile(IFormFile file);
        string GetUniqueFilePath(IFormFile file);
        Task WriteFileAsync(IFormFile file, string filePath);
        Task DeleteFileAsync(string filePath);
    }
}
