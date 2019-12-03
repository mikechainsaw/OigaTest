using OigaTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OigaTest.Entities
{
    public class DataSeeder
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Tenants.Any())
            {
                var tenants = new List<Tenant>()
                {
                    new Tenant { /*Id = 1,*/ Name = "Nintendo", Url="localhost:60000", isDefault=true},
                    new Tenant { /*Id = 2,*/ Name = "Capcom", Url="localhost:60001" ,isDefault=false},
                    new Tenant { /*Id = 3,*/ Name = "FromSoftWare", Url="localhost:60002" ,isDefault=false},
                };
                context.Tenants.AddRange(tenants);
                context.SaveChanges();
            }


            var tenantArray = context.Tenants.ToArray();
            if (!context.AppUsers.Any())
            {
                var users = new List<AppUser>()
                {
                    new AppUser { /*Id = 2,*/ UserName = "Michael", Password = "1234" ,Tenant=tenantArray[0],Role=EUserRole.Admin},
                    new AppUser { /*Id = 1,*/ UserName = "Ryu", Password = "1234" ,Tenant=tenantArray[1],Role=EUserRole.Admin},
                    new AppUser { /*Id = 1,*/ UserName = "Artorias", Password = "1234" ,Tenant=tenantArray[2],Role=EUserRole.Admin},
                };
                context.AppUsers.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}

