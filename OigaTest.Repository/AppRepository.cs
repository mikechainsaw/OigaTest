using Microsoft.EntityFrameworkCore;
using OigaTest.Entities;
using OigaTest.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OigaTest.Models
{
    public class AppRepository : IAppRepository
    {

        public AppRepository(AppDbContext context)
        {
            this.Context = context;
        }

        public AppDbContext Context { get; }

        public async Task<List<Tenant>> GetTenantsAsync()
        {
            return await Context.Tenants.ToListAsync();
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await Context.AppUsers.ToListAsync();
        }

        public List<Tenant> GetTenants()
        {
            return  Context.Tenants.ToList();
        }

        public List<AppUser> GetUsers()
        {
            return  Context.AppUsers.ToList();
        }

        public async Task<AppUser> CreateUsersAsync(AppUser user)
        {
            var addedUser= await Context.AppUsers.AddAsync(user);
            Context.SaveChanges();
            return addedUser?.Entity;
        }

        public async Task<AppUser> DeleteUsersAsync(AppUser user)
        {
            var deletedUser = Context.AppUsers.Remove(user);
            var row=await Context.SaveChangesAsync();
            return deletedUser?.Entity;
        }

        public async Task<AppUser> UpdateUsersAsync(AppUser user)
        {
            var updatedUser=Context.AppUsers.Update(user);
            var row = await Context.SaveChangesAsync();
            return updatedUser?.Entity;
        }

    }
}
