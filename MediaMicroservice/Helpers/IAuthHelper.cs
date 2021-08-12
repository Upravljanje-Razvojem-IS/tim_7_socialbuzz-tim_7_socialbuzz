using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.Helpers
{
    public interface IAuthHelper
    {
        public bool AuthorizeUser(string key);
    }
}
