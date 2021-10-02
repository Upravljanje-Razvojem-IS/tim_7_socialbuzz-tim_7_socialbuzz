using AccountService.Entities;
using AccountService.Exceptions;
using AccountService.Services.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private UserManager<Account> UserManager { get; set; }
        private readonly IRolesService _rolesService;

        public AccountService(UserManager<Account> userManager, IRolesService rolesService)
        {
            UserManager = userManager;
            _rolesService = rolesService;
        }

        public async Task<Account> AddAsync(Account account, string password, string roleId)
        {
            var role = await _rolesService.GetByIdAsync(Guid.Parse(roleId));

            account.Id = Guid.NewGuid();
            account.UserName = account.Email;
            account.Role = role;
            IdentityResult result = await UserManager.CreateAsync(account, password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(account, role.Name);
                return account;
            }
            else
            {
                throw new BadRequestException();
            }
        }

        public List<Account> GetAllAsync()
        {
            return UserManager.Users.ToList();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            var account = await UserManager.FindByIdAsync(id.ToString());

            if (account == null)
                throw new NotFoundException("User not found.");

            account.Role = await _rolesService.GetByIdAsync(account.RoleId);

            return account;
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            var account1 = await UserManager.FindByIdAsync(account.Id.ToString());
            if (account1 == null)
                throw new NotFoundException();

            account1.FirstName = account.FirstName;
            account1.LastName = account.LastName;
            account.UserName = account.Email;
            account1.StreetAddress = account.StreetAddress;
            account1.Email = account.Email;

            //var result = await _userManager.UpdateAsync(account1);

            //return result.Succeeded;
            return true;
        }

        public async Task<bool> RemoveByIdAsync(Guid id)
        {
            var account = await UserManager.FindByIdAsync(id.ToString());

            if (account == null)
                throw new NotFoundException("Account not found.");

            var result = await UserManager.DeleteAsync(account);

            return result.Succeeded;
        }
    }
}
