using AccountService.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountService.Services.Roles
{
    public interface IRolesService
    {
        List<AccRole> GetAllAsync();
        Task<AccRole> GetByIdAsync(Guid id);
        Task<AccRole> GetByNameAsync(string name);
        Task<AccRole> AddAsync(AccRole entity);
        Task<bool> UpdateAsync(AccRole entity);
        Task<bool> RemoveByIdAsync(Guid id);
    }
}
