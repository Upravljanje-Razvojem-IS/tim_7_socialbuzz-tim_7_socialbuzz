using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Helpers
{
    public interface IAuth
    {
        public bool AuthorizeUser(string key);
    }
}
