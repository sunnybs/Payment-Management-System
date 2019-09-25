using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Payment_Management_System.Models
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options)
            : base(options)
        { }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentPosition> PaymentPosition { get; set; }
    }
}
