
using Microsoft.AspNetCore.Http;
using OigaTest.Entities;
using OigaTest.Models;
using SaasKit.Multitenancy;
using System.Linq;
using System.Threading.Tasks;

namespace OigaTest.Web
{
    public class TenantResolver: ITenantResolver<Tenant>
    {
        private readonly AppDbContext _dbContext;

        public TenantResolver(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<TenantContext<Tenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<Tenant> tenantContext = null;
            var hostName = context.Request.Host.Value.ToLower();

            var tenant = _dbContext.Tenants.FirstOrDefault(
                t => t.Url.Equals(hostName))?? _dbContext.Tenants.First(o=>o.isDefault);

            if (tenant != null)
            {
                tenantContext = new TenantContext<Tenant>(tenant);
            }

            return Task.FromResult(tenantContext);
        }

    }
}
