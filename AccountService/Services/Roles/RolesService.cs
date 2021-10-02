using AccountService.Entities;
using AccountService.Exceptions;
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

        public List<AccRole> GetAllAsync()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<AccRole> GetByIdAsync(Guid id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                throw new NotFoundException();

            return role;
        }

        public async Task<AccRole> GetByNameAsync(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            if (role == null)
                throw new NotFoundException();

            return role;
        }

        public async Task<AccRole> AddAsync(AccRole role)
        {
            if (role.Name.Length < 1)
                throw new BadRequestException("Role name must be longer than 1 character.");

            var exists = await roleManager.RoleExistsAsync(role.Name);
            if (exists)
            {
                throw new ConflictException("Role already exists.");
            }

            await roleManager.CreateAsync(role);
            var newRole = await roleManager.FindByIdAsync(role.Id.ToString());

            return newRole;
        }

        public async Task<bool> RemoveByIdAsync(Guid id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                throw new NotFoundException();

            await roleManager.DeleteAsync(role);

            return true;
        }

        public async Task<bool> UpdateAsync(AccRole role)
        {
            if (role.Name.Length < 1)
                throw new BadRequestException("Role name must be longer than 1 character.");

            var exists = await roleManager.RoleExistsAsync(role.Name);
            if (!exists)
                throw new NotFoundException();

            var result = await roleManager.UpdateAsync(role);

            return result.Succeeded;
        }
    }
}
