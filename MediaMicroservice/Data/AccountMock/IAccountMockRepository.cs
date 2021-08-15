using MediaMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaMicroservice.Data.AccountMock
{
    public interface IAccountMockRepository
    {
        AccountDto GetAccountByFirstName(string firstName);
    }
}
