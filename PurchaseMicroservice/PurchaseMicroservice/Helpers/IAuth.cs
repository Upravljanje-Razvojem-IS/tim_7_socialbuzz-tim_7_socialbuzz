using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool AuthorizeUser(string key);
    }
}
