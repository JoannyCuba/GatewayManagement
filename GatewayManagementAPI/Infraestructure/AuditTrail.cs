using GatewayManagementCore.Entities;
using GatewayManagementCore.Interfaces;

namespace GatewayManagementAPI.Infraestructure
{
    public class AuditTrail : IAuditTrail
    {
        private readonly HttpContext httpContext;
        private readonly IUnitOfWork context;

        public AuditTrail(HttpContext httpContext, IUnitOfWork context)
        {
            this.httpContext = httpContext;
            this.context = context;
        }
        public async Task SaveAuditTrail(AuditTrailManager audit)
        {
            string userId;
            httpContext.Items.TryGetValue("UserId", out object userIdObject);
            if (userIdObject != null)
            {
                userId = userIdObject.ToString();
                audit.UserId = userId;
            }
            audit.Date = DateTime.Now;
            audit.Host = httpContext.Connection.RemoteIpAddress.ToString();
            context.Audit.Add(audit);
            await context.Save();
        }
    }
}
