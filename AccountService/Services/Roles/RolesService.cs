using AccountService.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Services.Roles
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<AccRole> roleManager;

        public RolesService(RoleManager<AccRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<AccRole> AddAsync(AccRole role)
        {
            if (!roleManager.RoleExistsAsync(role.Name).Result)
            {
                await roleManager.CreateAsync(role);
                return role;
            }
            throw new Exception("Role must be unique");
        }

        public List<AccRole> GetAllAsync()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<AccRole> GetByIdAsync(Guid id)
        {
            return await roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<bool> RemoveByIdAsync(Guid id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return false;

            await roleManager.DeleteAsync(role);

            return true;
        }

        public async Task<bool> UpdateAsync(AccRole role)
        {
            var exists = await roleManager.RoleExistsAsync(role.Name);
            if (!exists)
                return false;

            await roleManager.UpdateAsync(role);

            return true;
        }
    }
}
