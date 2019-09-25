using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Payment_Management_System.Models.Validators;

namespace Payment_Management_System.Models
{
    public class Payment 
    {
        public int PaymentId { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Необходимо ввести ФИО.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Необходимо выбрать тип платежа.")]
        public PaymentType PaymentType { get; set; }

        [Required(ErrorMessage = "Необходимо ввести сумму платежа.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [CorrectPaymentSum("ViewPaymentPositions")]
        public decimal PaymentSum { get; set; }
        public virtual IList<PaymentPosition> PaymentPositions { get; set; }
        [NotMapped]
        [MinPositionsCount]
        public virtual IDictionary<int, PaymentPosition> ViewPaymentPositions { get; set; }
    }

    public enum PaymentType
    {
        [Display(Name = "Тип платежа 1")]
        Salary,
        [Display(Name = "Тип платежа 2")]
        Investments
    }
}
