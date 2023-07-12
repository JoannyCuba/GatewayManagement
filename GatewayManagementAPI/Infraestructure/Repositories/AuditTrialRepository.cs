using AutoMapper;
using GatewayManagementAPI.Data;
using GatewayManagementCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GatewayManagementAPI.Infraestructure.Repositories
{
    public class AuditTrialRepository : Repository<AuditTrailManager>
    {
        private DbSet<Models.AuditTrailManager> dbSet;
        public AuditTrialRepository(ApplicationDbContext context) : base(context)
        {
            dbSet = context.Set<Models.AuditTrailManager>();
        }

        public override void Add(AuditTrailManager auditCore)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var audit = mapper.Map<Models.AuditTrailManager>(auditCore);
            dbSet.Add(audit);
        }
        public MapperConfiguration MapperConfiguration()
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<AuditTrailManager, Models.AuditTrailManager>();

            });
            return config;
        }
    }
}
