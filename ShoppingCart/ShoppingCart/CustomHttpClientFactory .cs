using System.Net.Http;
using Flurl.Http.Configuration;

namespace ShoppingCart
{
    public class CustomHttpClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (a,b,c,d) => true
            };
        }
    }
    
}
