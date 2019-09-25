using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment_Management_System.Services;

namespace Payment_Management_System.Models.Validators
{
    public class ValidateFileAttribute : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var fileManager = (IFileManager)validationContext.GetService(typeof(IFileManager));
            return value == null || fileManager.VerifyFile((IFormFile) value)
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage()
        {
            return $"Файл с данным разрешением не возможно загрузить.";
        }
    }
}
