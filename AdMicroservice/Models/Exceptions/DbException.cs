using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models.Exceptions
{
    [Serializable]
    public class DbException : Exception
    {
        public DbException(string message) : base(message)
        {

        }
    }
}
