using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OigaTest.Models;
using OigaTest.Models.Interfaces;
using OigaTest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OigaTest.UnitTests
{
    [TestClass]
    public class ServiceTests
    {
        private List<AppUser> users;
        private IAppRepository AppRepository;
        private Tenant defaultTenant;
        [TestInitialize]
        public void TestInitialize()
        {
            var mock = new Mock<IAppRepository>();
            defaultTenant = new Tenant() { isDefault = true, Name = "tenant", Url = "url" };
            users = new List<AppUser> { new AppUser { Id = 1, UserName = "JohnDoe" ,Tenant=defaultTenant},
                new AppUser { Id = 2, UserName = "Someone" }};
            mock.Setup(o => o.GetUsersAsync()).Returns(Task.FromResult(users));
            AppRepository = mock.Object;
        }

        [TestMethod]
        public async Task GetUserTest()
        {
            IAppService appService = new AppService(AppRepository);
            var users = await appService.GetUsers(defaultTenant);
            Assert.IsTrue(users.Any());

        }
    }
}
