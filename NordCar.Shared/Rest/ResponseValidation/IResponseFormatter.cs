using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest.ResponseValidation
{
    public interface IResponseFormatter
    {
        string FormatResponse(ResponseSimple response);
    }
}
