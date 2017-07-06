using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
    public class Response<T> : ResponseSimple
    {
        public T Content { get; set; }
    }
}
