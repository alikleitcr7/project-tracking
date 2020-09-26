using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Utils
{
    public class ValidationExtensions : IValidationExtensions
    {
        public bool ValidateAnnotations(object instance, out ICollection<ValidationResult> results)
        {
            ValidationContext ctx = new ValidationContext(instance);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, ctx, results, true);
            // Validates the object and its properties using the previously created context.
            // The variable isValid will be true if everything is valid
            // The results variable contains the results of the validation
        }
    }
}
