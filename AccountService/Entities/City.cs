using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Entities
{
    public class City
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
