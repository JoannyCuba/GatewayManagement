using AutoMapper;
using GatewayManagementAPI.Data;
using GatewayManagementAPI.Utils;
using GatewayManagementCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GatewayManagementAPI.Infraestructure.Repositories
{
    public class PeripheralDeviceRepository : Repository<PeripheralDevice>
    {
        private DbSet<Models.PeripheralDevice> dbSet;
        public PeripheralDeviceRepository(ApplicationDbContext context) : base(context)
        {
            dbSet = context.Set<Models.PeripheralDevice>();
        }

        public async override Task<List<PeripheralDevice>> FindPaginationAsync(int page = 1, int itemPerPage = 25, Expression<Func<PeripheralDevice, bool>> filter = null)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var filterApi = mapper.Map<Expression<Func<Models.PeripheralDevice, bool>>>(filter);

            IQueryable<Models.PeripheralDevice> query = dbSet;

            query = query.Where(filterApi);
            query.Skip((page - 1) * itemPerPage).Take(itemPerPage);
            var r = await query.ToListAsync();
            return mapper.Map<List<PeripheralDevice>>(r);
        }

        public async override Task<int> CountAsync(Expression<Func<PeripheralDevice, bool>> filter = null)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var filterApi = mapper.Map<Expression<Func<Models.PeripheralDevice, bool>>>(filter);

            IQueryable<Models.PeripheralDevice> query = dbSet;
            if (filterApi == null)
                return await query.CountAsync();
            return await query.Where(filterApi).CountAsync();
        }

        public async override Task<PeripheralDevice> FindOneAsync(Expression<Func<PeripheralDevice, bool>> filter = null)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var filterApi = mapper.Map<Expression<Func<Models.PeripheralDevice, bool>>>(filter);

            IQueryable<Models.PeripheralDevice> query = dbSet;
            var r = await query.FirstOrDefaultAsync(filterApi);
            return mapper.Map<PeripheralDevice>(r);
        }
        public override void Add(PeripheralDevice gatewayCore)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var gateway = mapper.Map<Models.PeripheralDevice>(gatewayCore);
            dbSet.Add(gateway);
        }

        public async override void Update(PeripheralDevice deviceCore)
        {
            IQueryable<Models.PeripheralDevice> query = dbSet;

            var device = query.FirstOrDefault(x => x.Id == deviceCore.Id);
            device.Id = deviceCore.Id;
            device.Vendor = deviceCore.Vendor;
            device.DateCreated = deviceCore.DateCreated;
            device.UID= deviceCore.UID;
            device.IsOnline = deviceCore.IsOnline;
            device.IsActive = deviceCore.IsActive;
            device.GatewayId = deviceCore?.GatewayId;

            dbSet.Update(device);
        }
        public override void Remove(PeripheralDevice gatewayCore)
        {
            IQueryable<Models.PeripheralDevice> query = dbSet;
            var gateway = query.FirstOrDefault(x => x.Id == gatewayCore.Id);
            if (gateway == null) return;
            dbSet.Remove(gateway);
        }
        public async override void Delete(Expression<Func<PeripheralDevice, bool>> filter = null)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var filterApi = mapper.Map<Expression<Func<Models.PeripheralDevice, bool>>>(filter);

            IQueryable<Models.PeripheralDevice> query = dbSet;

            var gateway = query.FirstOrDefault(filterApi);
            gateway.IsActive = false;
            dbSet.Update(gateway);
        }

        public MapperConfiguration MapperConfiguration()
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<PeripheralDevice, Models.PeripheralDevice>();
                cfg.CreateMap<Models.PeripheralDevice, PeripheralDevice>();

                cfg.CreateMap<Expression<Func<PeripheralDevice, bool>>, Expression<Func<Models.PeripheralDevice, bool>>>().ConvertUsing<ExpressionDeviceConverter>();

            });
            return config;
        }
    }
}
