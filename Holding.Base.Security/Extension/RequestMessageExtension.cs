using System.Linq;
using System.Net.Http;

namespace Holding.Base.Security.Extension
{
    public static class RequestMessageExtension
    {
        public static bool HasToken(this HttpRequestMessage request)
        {
            return request.Headers.Contains("Token");
        }

        public static string ExtractToken(this HttpRequestMessage request)
        {
            return request.Headers.GetValues("Token").ToArray().ElementAt(0).Replace("\"", string.Empty);
        }
    }
}