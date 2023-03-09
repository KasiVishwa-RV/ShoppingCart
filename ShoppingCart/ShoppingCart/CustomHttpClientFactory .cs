using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Flurl.Http.Configuration;
using ShoppingCart.Services.Interfaces;

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
