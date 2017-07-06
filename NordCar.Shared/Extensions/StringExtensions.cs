using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace System
{
    //JRJ: Candidate for Ed.Shared.Extensions, if its proved useful enough.
    public static class StringExtensions
    {
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string UrlEncode(this string plainText)
        {
            return HttpUtility.UrlEncode(plainText);
        }

        public static string UrlDecode(this string urlText)
        {
            return HttpUtility.UrlDecode(urlText);
        }

        public static T TryParseJson<T>(this string json) where T : new()
        {
            if (!json.TrimStart().StartsWith("{"))
                return default(T);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Truncate(this string s, int maxLength)
        {
            return s != null && s.Length > maxLength ? s.Substring(0, maxLength) : s;
        }

        public static string EnsureNotNullAndTruncate(this string s, int maxLength)
        {
            return string.IsNullOrEmpty(s) ? "" : Truncate(s, maxLength);
        }

        public static string GetLettersOrDigits(this string s, int startPosition)
        {
            for (int index = startPosition; index < s.Length; index++)
            {
                if (!char.IsLetterOrDigit(s[index]))
                    return s.Substring(startPosition, index - startPosition);
            }
            return s.Substring(startPosition);
        }
    }
}

