using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
    public class UriFormatter
    {
        public static string FormatUriAsExtension(string uriExtension)
        {
            // Front slash is illegal, so strip it.
            var readyAsExtension = uriExtension.StartsWith("/") ? uriExtension.TrimStart('/') : uriExtension;
            return readyAsExtension;
        }

        public static string FormatUri(Uri absoluteUri, string uriExtension)
        {
            return absoluteUri.AbsolutePath + uriExtension;
        }
    }
}
