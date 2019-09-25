using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payment_Management_System.Models;

namespace Payment_Management_System.ViewModels
{
    public class PaymentViewModel
    {
        public Payment Payment { get; set; }
        public IEnumerable<PaymentPosition> PaymentPositions { get; set; }
    }
}
