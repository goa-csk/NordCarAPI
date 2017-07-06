using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Messages
{
    public class ResponsePackage
    {
        public List<string> Errors { get; set; }

        public object Result { get; set; }
        public ResponsePackage(object result, List<string> errors)
        {
            Errors = errors;
            Result = result;
        }
    }

    public class ResponsePackageError
    {
        public List<string> Errors { get; set; }


        public ResponsePackageError(List<string> errors)
        {
            Errors = errors;

        }
    }


    public class ResponsePackageSucces
    {


        public object Result { get; set; }
        public string Status { get; set; }
        public ResponsePackageSucces(object result)
        {
            Result = result;
            Status = "OK";
        }
    }
}