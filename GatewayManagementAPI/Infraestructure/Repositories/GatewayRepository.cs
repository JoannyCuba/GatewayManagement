using AutoMapper;
using GatewayManagementAPI.Data;
using GatewayManagementAPI.Utils;
using GatewayManagementCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GatewayManagementAPI.Infraestructure.Repositories
{
    public class GatewayRepository : Repository<Gateway>
    {
        private DbSet<Models.Gateway> dbSet;
        public GatewayRepository(ApplicationDbContext context) : base(context)
        {
            dbSet = context.Set<Models.Gateway>();
        }
        public async override Task<List<Gateway>> FindPaginationAsync(int page = 1, int itemPerPage = 25, Expression<Func<Gateway, bool>> filter = null)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var filterApi = mapper.Map<Expression<Func<Models.Gateway, bool>>>(filter);

            IQueryable<Models.Gateway> query = dbSet;

            query = query.Where(filterApi);
            query.Skip((page - 1) * itemPerPage).Take(itemPerPage);
            var r = await query.ToListAsync();
            return mapper.Map<List<Gateway>>(r);
        }

        public async override Task<int> CountAsync(Expression<Func<Gateway, bool>> filter = null)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var filterApi = mapper.Map<Expression<Func<Models.Gateway, bool>>>(filter);

            IQueryable<Models.Gateway> query = dbSet;
            if (filterApi == null)
                return await query.CountAsync();
            return await query.Where(filterApi).CountAsync();
        }

        public async override Task<Gateway> FindOneAsync(Expression<Func<Gateway, bool>> filter = null)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var filterApi = mapper.Map<Expression<Func<Models.Gateway, bool>>>(filter);

            IQueryable<Models.Gateway> query = dbSet;
            query = query.Include(x => x.PeripheralDevices);
            var r = await query.FirstOrDefaultAsync(filterApi);
            return mapper.Map<Gateway>(r);
        }
        public override void Add(Gateway gatewayCore)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var gateway = mapper.Map<Models.Gateway>(gatewayCore);
            dbSet.Add(gateway);
        }

        public async override void Update(Gateway gatewayCore)
        {
            IQueryable<Models.Gateway> query = dbSet;

            var gateway = query.FirstOrDefault(x => x.Id == gatewayCore.Id);
            gateway.Id = gatewayCore.Id;
            gateway.SerialNumber = gatewayCore.SerialNumber;
            gateway.Name = gatewayCore.Name;
            gateway.IPAddress = gatewayCore.IPAddress;
            gateway.IsActive = gatewayCore.IsActive;

            dbSet.Update(gateway);
        }
        public override void Remove(Gateway gatewayCore)
        {
            IQueryable<Models.Gateway> query = dbSet;
            var gateway = query.FirstOrDefault(x => x.Id == gatewayCore.Id);
            if (gateway == null) return;
            dbSet.Remove(gateway);
        }
        public async override void Delete(Expression<Func<Gateway, bool>> filter = null)
        {
            MapperConfiguration config = MapperConfiguration();
            var mapper = config.CreateMapper();
            var filterApi = mapper.Map<Expression<Func<Models.Gateway, bool>>>(filter);

            IQueryable<Models.Gateway> query = dbSet;

            var gateway = query.FirstOrDefault(filterApi);
            gateway.IsActive = false;
            dbSet.Update(gateway);
        }

        public MapperConfiguration MapperConfiguration()
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Gateway, Models.Gateway>()
                .ForMember(x => x.PeripheralDevices, opciones => opciones.MapFrom(MapPeripheralDevices));
                cfg.CreateMap<Models.Gateway, Gateway>()
                .ForMember(x => x.PeripheralDevices, opciones => opciones.MapFrom(y => y.PeripheralDevices));


                cfg.CreateMap<Expression<Func<Gateway, bool>>, Expression<Func<Models.Gateway, bool>>>().ConvertUsing<ExpressionGatewayConverter>();

            });
            return config;
        }
        private static List<Models.PeripheralDevice> MapPeripheralDevices(Gateway gatC, Models.Gateway gatA)
        {
            List<Models.PeripheralDevice> result = new();
            if (gatC.PeripheralDevices == null) { return result; }
            foreach (var oneG in gatC.PeripheralDevices)
            {
                result.Add(new Models.PeripheralDevice
                {
                    Id = oneG.Id,
                    UID = oneG.UID,
                    Vendor = oneG.Vendor,
                    DateCreated = oneG.DateCreated,
                    IsOnline = oneG.IsOnline,
                    IsActive = oneG.IsActive,
                    GatewayId = oneG.GatewayId,
                });
            }
            return result;
        }
    }
}
