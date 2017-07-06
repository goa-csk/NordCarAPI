using Ed.Shared.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThrowErrors(this IList<IDomValueValidationResult> validationResults)
        {
            if (validationResults.Any())
                throw new Exception();//DomValueValidationException(validationResults);
        }
    }
}
