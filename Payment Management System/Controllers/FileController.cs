using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Payment_Management_System.Controllers
{
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;

        public FileController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult Download(string filePath, string fileName)
        {
            var path = _appEnvironment.WebRootPath + filePath;
            if (!System.IO.File.Exists(path)) return NotFound();

            var fs = new FileStream(path, FileMode.Open);
            var fileType = "application/octet-stream";
            return File(fs, fileType, fileName);
        }
    }
}
