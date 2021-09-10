using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class Auth : IAuth
    {

        private readonly IConfiguration configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>

        public Auth(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool AuthorizeUser(string key)
        {
            if (!key.StartsWith("Bearer"))
            {
                return false;
            }

            var secretKey = key.Substring(key.IndexOf("Bearer") + 7);
            var storedKey = configuration.GetValue<string>("Authorization:Key");

            if (storedKey != secretKey)
            {
                return false;
            }
            return true;
        }
    }
}
