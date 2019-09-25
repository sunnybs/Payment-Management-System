using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Payment_Management_System.Models;
using Payment_Management_System.Services;

namespace Payment_Management_System.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _context;
        private readonly IFileManager _fileManager;

        public PaymentRepository(PaymentContext context, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _context = context;
        }

        public void Insert(Payment payment)
        {
            payment.Date = DateTime.Now;
            _context.Payments.Add(payment);
            payment.PaymentPositions = payment.ViewPaymentPositions.Values.ToList();
            foreach (var paymentPosition in payment.PaymentPositions)
            {
                SaveNestedFiles(paymentPosition);
                _context.PaymentPosition.Add(paymentPosition);
            }
        }

        public Payment Get(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null) return null;
            var paymentPositions = _context.PaymentPosition.Where(pos => pos.PaymentId == id);
            payment.ViewPaymentPositions = paymentPositions.ToDictionary(pos => pos.PaymentPositionId);
            return payment;
        }

        public void Delete(Payment payment)
        {
            var paymentPositions = _context.PaymentPosition.Where(pos => pos.PaymentId == payment.PaymentId);
            DeleteNestedFiles(paymentPositions);
            _context.PaymentPosition.RemoveRange(paymentPositions);
            _context.Payments.Remove(payment);
        }

        public void Update(Payment oldValue, Payment newValue)
        {
            var oldPaymentPositions = _context.PaymentPosition.Where(pos => pos.PaymentId == oldValue.PaymentId);
            DeleteNestedFiles(oldPaymentPositions);
            _context.PaymentPosition.RemoveRange(oldPaymentPositions);

            oldValue.Date = DateTime.Now;
            oldValue.Name = newValue.Name;
            oldValue.PaymentSum = newValue.PaymentSum;
            oldValue.PaymentType = newValue.PaymentType;
            oldValue.PaymentPositions = newValue.ViewPaymentPositions.Values.ToList();

            foreach (var paymentPosition in oldValue.PaymentPositions)
            {
                SaveNestedFiles(paymentPosition);
                _context.PaymentPosition.Add(paymentPosition);
            }
        }

        public IEnumerable<Payment> FindAll(Expression<Func<Payment, bool>> filter)
        {
            return _context.Payments.Where(filter);
        }

        public IEnumerable<Payment> FindAll()
        {
            return _context.Payments;
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private void DeleteNestedFiles(IQueryable<PaymentPosition> paymentPositions)
        {
            if (paymentPositions != null)
            foreach (var paymentPos in paymentPositions)
                if (paymentPos.NestedFilePath != null)
                    _fileManager.DeleteFileAsync(paymentPos.NestedFilePath);
        }

        private void SaveNestedFiles(PaymentPosition paymentPosition)
        {
            if (paymentPosition.FileUpload.Value == null) return;
            var file = paymentPosition.FileUpload.Value;
            var path = _fileManager.GetUniqueFilePath(file);
            _fileManager.WriteFileAsync(file, path);
            paymentPosition.NestedFileName = file.FileName;
            paymentPosition.NestedFilePath = path;
        }
    }
}