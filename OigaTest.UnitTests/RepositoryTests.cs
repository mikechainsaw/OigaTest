using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OigaTest.Entities;
using OigaTest.Models;
using OigaTest.Models.Interfaces;
using OigaTest.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OigaTest.UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        private static DbSet<T> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var MockSet = new Mock<DbSet<T>>();
            MockSet.As<IDbAsyncEnumerable<T>>().Setup(x => x.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(queryable.GetEnumerator()));
            MockSet.As<IQueryable<T>>().Setup(x => x.Provider).Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));
            MockSet.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryable.Expression);
            MockSet.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryable.ElementType);
            MockSet.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(queryable.GetEnumerator());


            return MockSet.Object;
        }
        private AppDbContext MockDbCOntext;

        [TestInitialize]
        public void TestInitialize()
        {          
            var mockCOntext = new Mock<AppDbContext>();
            mockCOntext.Reset();
            var tenantmock = GetQueryableMockDbSet(
                new Tenant { Id = 1, Name = "Tenant1", Url = "urlT1" },
                new Tenant { Id = 2, Name = "Tenant2", Url = "urlT2" });
            mockCOntext.Setup(c => c.Tenants).Returns(tenantmock);
            mockCOntext.Setup(c => c.AppUsers).Returns(GetQueryableMockDbSet(
                new AppUser { Id = 1, UserName = "JohnDoe" },
                new AppUser { Id = 2, UserName = "Someone" }));           
                MockDbCOntext = mockCOntext.Object;
        }

        [TestMethod]
        public async Task GetTenantsAsyncTest()
        {
            IAppRepository tenantsRepo = new AppRepository(MockDbCOntext);
            var tenants = await tenantsRepo.GetTenantsAsync();
            Assert.IsTrue(tenants.Any());

        }

        [TestMethod]
        public void GetTenantsTest()
        {
            IAppRepository tenantsRepo = new AppRepository(MockDbCOntext);
            var tenants = tenantsRepo.GetTenants();
            Assert.IsTrue(tenants.Any());

        }

        [TestMethod]
        public async Task CreateUserTest()
        {
            IAppRepository repo = new AppRepository(MockDbCOntext);
            var user = new AppUser { UserName = "user", Password = "pass" };
            var userCreated =await  repo.CreateUsersAsync(user);
            Assert.IsTrue(true);

        }
    }

}
