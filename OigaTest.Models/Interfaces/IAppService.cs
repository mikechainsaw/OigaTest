using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OigaTest.Models.Interfaces
{
    public interface IAppService
    {
        Task<AppUser> CreateUser(AppUser appUser);
        Task<AppUser> DeleteUser(int appUserId);
        Task<AppUser> GetUser(int appUserId);
        Task<List<AppUser>> GetUsers(Tenant tenantId);

        Task<AppUser> IsUserValid(string userName, string password, Tenant tenant);
        Task<AppUser> UpdateUser(AppUser appUSer);
    }
}
