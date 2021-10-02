using AccountService.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountService.Services.Accounts
{
    public interface IAccountService
    {
        List<Account> GetAllAsync();
        Task<Account> GetByIdAsync(Guid id);
        Task<Account> AddAsync(Account entity, string password, string roleId);
        Task<bool> UpdateAsync(Account entity);
        Task<bool> RemoveByIdAsync(Guid id);
    }
}
