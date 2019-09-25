using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment_Management_System.ViewModels
{
    public class PaymentsWithFilterViewModel
    {
        public IEnumerable<Payment_Management_System.Models.Payment> Payments { get; set; }
        public FilterViewModel Filter { get; set; }

    }
}
