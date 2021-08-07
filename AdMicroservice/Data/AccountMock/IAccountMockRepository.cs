using AdMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data.AccountMock
{
    public interface IAccountMockRepository
    {
        AccountDto GetAccountByFirstName(string firstName);
    }
}
