using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment_Management_System.Models.Validators;

namespace Payment_Management_System.Models
{
    public class PaymentPosition
    {
        public int PaymentPositionId { get; set; }

        [Required(ErrorMessage = "Необходимо ввести сумму платежа")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaymentPositionSum { get; set; }
        public string Comment { get; set; }
        public string NestedFilePath { get; set; }
        public string NestedFileName { get; set; }

        [NotMapped]
        public FormFileWrapper FileUpload { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

    }

    public class FormFileWrapper
    {
        [ValidateFile]
        public IFormFile Value { get; set; }
    }
}
