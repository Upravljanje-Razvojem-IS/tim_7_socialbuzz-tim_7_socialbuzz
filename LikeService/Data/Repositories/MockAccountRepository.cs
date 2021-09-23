using LikeService.Data.Interfaces;
using LikeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeService.Data.Repositories
{
    public class MockAccountRepository : IAccountRepository
    {
        private static List<Account> _accounts = new List<Account>
        {
            new Account
            {
                AccountUid = Guid.Parse("601e83bd-def5-4930-a23e-ce992a380a08"),
                FirstName = "Pera",
                LastName = "Peric"
            },

            new Account
            {
                AccountUid = Guid.Parse("3064217b-42fd-4d61-982e-d7eccb45f1d6"),
                FirstName = "Nikola",
                LastName = "Nikolic"
            }
        };

        public Account Get(Guid accoutnUid)
        {
            return _accounts.Where(a => a.AccountUid == accoutnUid).SingleOrDefault();
        }
    }
}
