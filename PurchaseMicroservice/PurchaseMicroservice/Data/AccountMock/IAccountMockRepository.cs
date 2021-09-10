using PurchaseMicroservice.Models.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseMicroservice.Data.AccountMock
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountMockRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AccountDTO GetAccountByID(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        AccountDTO GetAccountByFirstName(string firstName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        AccountDTO GetAccountByLasttName(string lastName);
    }
}
