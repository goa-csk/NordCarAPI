using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
    public class TokenCreator
    {
        private readonly BaseAddressResolver _baseAddressResolver;

        public TokenCreator(BaseAddressResolver baseAddressResolver)
        {
            _baseAddressResolver = baseAddressResolver;
        }

        public async Task<string> RequestTokenAsync(SupportedServices serviceToCall)
        {
            var payload = "grant_type=password&username={EC}&password={EC}";
            var content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await PostAsyncStringContent("Token", content, serviceToCall);
            dynamic tokenPayload = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return tokenPayload.access_token;
        }

        private async Task<HttpResponseMessage> PostAsyncStringContent(
            string uriExtension, StringContent stringContent,
            SupportedServices serviceToCall) 
        {
            uriExtension = UriFormatter.FormatUriAsExtension(uriExtension);
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddressResolver.ResolveBaseAddress(serviceToCall);
                return await client.PostAsync(uriExtension, stringContent);
            }
        }
    }
}
