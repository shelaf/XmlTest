using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XmlTest
{
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = null;

            if (value is IEnumerable)
            {
                foreach(object o in (IEnumerable)value)
                {
                    context = new ValidationContext(o, null, null);
                    Validator.TryValidateObject(o, context, results, true);
                }
            }
            else
            {
                context = new ValidationContext(value, null, null);
                Validator.TryValidateObject(value, context, results, true);
            }
            if (results.Count > 0)
            {
                string message = string.Empty;
                foreach (ValidationResult result in results) {
                    message += result.ErrorMessage + "\r\n";
                }
                return new ValidationResult(message);
            }

            return ValidationResult.Success;
        }
    }
}
