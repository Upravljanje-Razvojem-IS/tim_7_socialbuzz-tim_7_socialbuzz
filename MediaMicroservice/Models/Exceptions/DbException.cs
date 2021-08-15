using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.Models.Exceptions
{
    public class DbException : Exception
    {
        public DbException(string message) : base(message)
        {

        }
    }
}
