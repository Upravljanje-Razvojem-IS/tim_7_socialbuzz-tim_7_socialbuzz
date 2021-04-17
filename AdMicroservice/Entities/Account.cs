using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    public class Account
    {
        public Guid accountID { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
    }
}
