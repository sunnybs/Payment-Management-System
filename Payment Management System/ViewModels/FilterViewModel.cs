using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Payment_Management_System.Models;

namespace Payment_Management_System.ViewModels
{
    public class FilterViewModel
    {
        public DateTime? From { get; set; }
        public DateTime? Until { get; set; }

        public decimal? MinPaymentSum { get; set; }
        public decimal? MaxPaymentSum { get; set; }

        public string NameToFind { get; set; }

        public PaymentType? PaymentTypeToFind { get; set; }

        public IEnumerable<Func<Payment, bool>> GetFilters()
        {
            var filters = new List<Func<Payment, bool>>();
            if (From != null)
                filters.Add((payment => payment.Date >= From));
            if (Until != null)
                filters.Add((payment => payment.Date <= Until));
            if (MinPaymentSum != null)
                filters.Add((payment => payment.PaymentSum >= MinPaymentSum));
            if (MaxPaymentSum != null)
                filters.Add((payment => payment.PaymentSum <= MaxPaymentSum));
            if (NameToFind != null)
                filters.Add((payment => payment.Name == NameToFind));
            if (PaymentTypeToFind != null)
                filters.Add((payment => payment.PaymentType == PaymentTypeToFind));
            return filters;
        }
    }
}
