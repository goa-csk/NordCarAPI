using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
    public class ResponseFactory
    {
         public static async Task<Response<TReturn>> GenerateResponse<TReturn>(HttpResponseMessage response)
        {
            //TODO: <jnl> services can now return a partial result, so we should not assume all messages were rejected
            if (response.IsSuccessStatusCode)
            {
                var returnObject = await response.Content.ReadAsAsync<TReturn>();
                return new Response<TReturn>
                {
                    IsSuccess = true,
                    StatusCode = response.StatusCode,
                    Content = returnObject
                };
            }

            return new Response<TReturn>
            {
                IsSuccess = false,
                StatusCode = response.StatusCode,
                Reason = response.ReasonPhrase,
                FailureContent = await response.Content.ReadAsStringAsync()
            };
        }

        public static async Task<Response<string>> GenerateResponseSimple(HttpResponseMessage response) //TODO MHT : 09-09-2016 - change to return ResponseSimple
        {
            //TODO: <jnl> services can now return a partial result, so we should not assume all messages were rejected
            if (response.IsSuccessStatusCode)
            {
                return new Response<string>
                {
                    IsSuccess = true,
                    StatusCode = response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }

            return new Response<string>
            {
                IsSuccess = false,
                Reason = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                FailureContent = await response.Content.ReadAsStringAsync()
            };
        }
    }
    
}
