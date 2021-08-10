using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.Exceptions
{
    public class DbExceptions : Exception
    {
        public DbExceptions(string message) : base(message)
        {

        }
    }
}
