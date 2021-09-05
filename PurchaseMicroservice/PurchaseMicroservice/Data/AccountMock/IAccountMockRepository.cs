using PurchaseMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.AccountMock
{
    public interface IAccountMockRepository
    {
        AccountDTO GetAccountByID(Guid id);
        AccountDTO GetAccountByFirstName(string firstName);
        AccountDTO GetAccountByLasttName(string lastName);
    }
}
