using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;
using TodoCSharp.RoleDao;
using TodoCSharp.Dao;

namespace TodoCSharp.RolePresentationService
{
    public class RolePresentationService : IRolePresentationService
    {
        private readonly IRoleDao roleDao;
        public RolePresentationService(IRoleDao roleDao)
        {
            this.roleDao = roleDao;
        }

        public IQueryable<IdentityRole> Roles =>
            roleDao.Roles;

        public async Task<IdentityResult> Create(String name) =>
            await roleDao.CreateAsync(new IdentityRole(name));

        public async Task<IdentityResult> Delete(IdentityRole role) =>
            await roleDao.DeleteAsync(role);

        public async Task<IdentityResult> Edit(String name, IdentityRole role)
        {
            role.Name = name;
            return await roleDao.UpdateAsync(role);
        }
            
        public async Task<IdentityRole> FindById(String id) =>
            await roleDao.FindByIdAsync(id);
    }
}
