using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Models.Mock
{
    public class AccountDTO
    {
        /// <summary>
        /// An identifier for the user account
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public String LastName { get; set; }
        public Double Balance { get; set; }
        public String Currency { get; set; }
    }
}
