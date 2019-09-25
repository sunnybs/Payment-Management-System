using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Payment_Management_System.Models.Validators
{
    public class CorrectPaymentSum : RequiredAttribute
    {
        private readonly string _positionsField;

        public CorrectPaymentSum(string positionsField)
        {
            _positionsField = positionsField;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var positionsFieldName = validationContext.ObjectType.GetProperty(_positionsField);
            if (positionsFieldName == null)
                return new ValidationResult(GetErrorMessage());

            var positionsValue =
                positionsFieldName.GetValue(validationContext.ObjectInstance,
                    null) as IDictionary<int, PaymentPosition>;

            if (positionsValue == null ||
                Math.Abs((decimal) value - positionsValue.Values.Select(pos => pos.PaymentPositionSum).Sum()) >
                (decimal) 0.01)
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return "Общая сумма по позициям не совпадает с заявленной суммой платежа.";
        }
    }
}