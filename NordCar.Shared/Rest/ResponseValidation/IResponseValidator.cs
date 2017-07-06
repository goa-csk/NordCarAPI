using Ed.Shared.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest.ResponseValidation
{
    public interface IResponseValidator
    {
        Task<TResult> Invoke<TResult>(IList<ValidationResponse> validationResponses, Func<Task<TResult>> methodToInvoke)
            where TResult : ResponseSimple, new();
    }
}
