using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Payment_Management_System.Models;

namespace Payment_Management_System.Repositories
{
    public interface IPaymentRepository
    {
        void Insert(Payment payment);
        Payment Get(int id);
        void Delete(Payment payment);
        void Update(Payment oldValue, Payment newValue);
        IEnumerable<Payment> FindAll(Expression<Func<Payment, bool>> filter);
        IEnumerable<Payment> FindAll();
        void Save();
    }
}
