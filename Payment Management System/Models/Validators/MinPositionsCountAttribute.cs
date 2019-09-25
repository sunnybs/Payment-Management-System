using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payment_Management_System.Models.Validators
{
    public class MinPositionsCountAttribute : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var positions = (IDictionary<int, PaymentPosition>) value;
            return (positions == null || positions.Count < 1)
                ? new ValidationResult(GetErrorMessage())
                : ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Должна быть как минимум 1 позиция.";
        }
    }
}
