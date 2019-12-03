using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OigaTest.Models.Interfaces
{
    public interface IAppRepository
    {
        Task<List<Tenant>> GetTenantsAsync();
        List<Tenant> GetTenants();

        Task<List<AppUser>> GetUsersAsync();
        List<AppUser> GetUsers();


        Task<AppUser> CreateUsersAsync(AppUser user);
        Task<AppUser> UpdateUsersAsync(AppUser user);

        Task<AppUser> DeleteUsersAsync(AppUser user);

    }
}
