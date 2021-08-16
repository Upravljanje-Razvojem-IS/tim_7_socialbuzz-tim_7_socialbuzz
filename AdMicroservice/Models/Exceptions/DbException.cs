using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdMicroservice.Models.Exceptions
{
    [Serializable]
    public class DbException : Exception
    {
        protected DbException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public DbException(string message) : base(message)
        {

        }
    }
}
