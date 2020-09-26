using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.Utils
{
    public interface IValidationExtensions
    {
        bool ValidateAnnotations(object instance, out ICollection<ValidationResult> results);
    }
}