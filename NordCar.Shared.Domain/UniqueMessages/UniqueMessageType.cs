using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Domain.UniqueMessages
{
    public enum UniqueMessageType
    {
        UserInfo = 0,
        ValidationError = 1,
        SystemError = 2,
    }
}
