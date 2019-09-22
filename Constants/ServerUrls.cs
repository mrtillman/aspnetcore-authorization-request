using System;
using System.Collections.Generic;

namespace AuthDemo.Constants
{
    public static class ServerUrls
    {
        public static Dictionary<ENV, string> API 
            => new Dictionary<ENV, string> {
                { ENV.DEV, "http://counter-culture:4000" },
                { ENV.PROD, "https://api.counter-culture.io"}
            };

        public static Dictionary<ENV, string> APP
            => new Dictionary<ENV, string> {
                { ENV.DEV, "http://counter-culture:8080" },
                { ENV.PROD, "https://www.counter-culture.io"}
            };

        public static Dictionary<ENV, string> DEV
            => new Dictionary<ENV, string> {
                { ENV.DEV, "http://counter-culture:9000" },
                { ENV.PROD, "https://geeks.counter-culture.io"}
            };

        public static Dictionary<ENV, string> SECURE 
            => new Dictionary<ENV, string> {
                { ENV.DEV, "http://counter-culture:5000" },
                { ENV.PROD, "https://secure.counter-culture.io"}
            };
    }
}