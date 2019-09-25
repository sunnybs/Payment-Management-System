using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Payment_Management_System.Services
{
    public class FileManager : IFileManager
    {
        private readonly IEnumerable<string> _allowedExtensions;
        private readonly IHostingEnvironment _appEnvironment;

        public FileManager(IConfiguration config, IHostingEnvironment appEnvironment)
        {
            _allowedExtensions = config.GetSection("AllowedFileExtensionsToUpload").AsEnumerable()
                .Select(item => item.Value);
            _appEnvironment = appEnvironment;
        }

        public bool VerifyFile(IFormFile file)
            => _allowedExtensions.Contains(Path.GetExtension(file.FileName));


        public string GetUniqueFilePath(IFormFile file)
            => "/files/" + Path.GetRandomFileName();


        public async Task WriteFileAsync(IFormFile file, string filePath)
        {
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public async Task DeleteFileAsync(string filePath)
            => await Task.Run(() => File.Delete(_appEnvironment.WebRootPath + filePath));
    }
}
