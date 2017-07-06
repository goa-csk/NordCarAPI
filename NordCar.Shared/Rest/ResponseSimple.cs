using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
     public class ResponseSimple
    {
        public bool IsSuccess { get; set; }
        public string Reason { get; set; }
        public string FailureContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public override string ToString()
        {
            //return $"IsSuccess: {IsSuccess}, Reason: {Reason}, FailureContent: {FailureContent}, StatusCode: {StatusCode}";
            return string.Format("IsSuccess: {0}, Reason: {1}, FailureContent: {2}, StatusCode: {3}", IsSuccess, Reason, FailureContent, StatusCode); 
        }
    }
}
