using OigaTest.Models;
using OigaTest.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OigaTest.Service
{
    public class AppService : IAppService
    {
        private readonly IAppRepository appRepo;
        public AppService(IAppRepository repo)
        {
            appRepo = repo;
        }

        public async Task<AppUser> CreateUser(AppUser appUser)
        {
            var result = await appRepo.CreateUsersAsync(appUser);
            return result;
        }

        public async Task<AppUser> DeleteUser(int appUserId)
        {
            var users = await appRepo.GetUsersAsync();
            var user = users.FirstOrDefault(o => o.Id == appUserId);
            var result = await appRepo.DeleteUsersAsync(user);
            return result;
        }

        public async Task<AppUser> GetUser(int appUserId)
        {
            var users = await appRepo.GetUsersAsync();
            var user = users.FirstOrDefault(o => o.Id == appUserId);
            return user;
        }

        public async Task<List<AppUser>> GetUsers(Tenant tenant)
        {
            var result = await appRepo.GetUsersAsync();
            return result.Where(o=>o.Tenant== tenant).ToList();
        }

        public async Task<AppUser> IsUserValid(string userName,string password,Tenant tenant)
        {
            var users= await appRepo.GetUsersAsync();
            var user = users.FirstOrDefault(o => o.Tenant== tenant && o.UserName == userName);
            if(user != null && user.Password == password)
                return user;
            return null;
        }

        public async Task<AppUser> UpdateUser(AppUser appUSer)
        {
            var users = await appRepo.GetUsersAsync();
            var user = users.FirstOrDefault(o => o.Id == appUSer.Id);
            if (string.IsNullOrEmpty(appUSer.Password))
                appUSer.Password = user.Password;
            var result = await appRepo.UpdateUsersAsync(appUSer);
            return result;
        }
    }
}
