﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Helpers
{
    public interface IAuthHelper
    {
        public bool AuthorizeUser(string key);
    }
}
