using GatewayManagementCore.Entities;
using GatewayManagementCore.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Infraestructure;
using Xunit;
using EventHandler = UnitTest.Infraestructure.EventHandler;

namespace UnitTest.Test
{
    public class GatewayTest
    {
        [Theory]
        //empty serial number
        [InlineData("","Gateway 1", "10.10.1.1", true)]
        //empty ip address
        [InlineData("4217eaed-2ae3-470b-a116-97e63da99827", "Gateway 1", "", true)]
       //incorrect format Ipv4 ip address
        [InlineData("4217eaed-2ae3-470b-a116-97e63da99827", "Gateway 1", "352.514.874.5", true)]
        public async Task Add_Invalid_Argument(string serial, string? name, string ipAddress, bool IsActive)
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCGateway uCGateway = new UCGateway(_unitOfWork, new EventHandler());
                await Assert.ThrowsAnyAsync<Exception>(() => uCGateway.Create(serial, name,ipAddress, IsActive, null));
            }
        }

        [Fact]
        public async Task Add_Correctly()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCGateway uCGateway = new UCGateway(_unitOfWork, new EventHandler());
                Gateway gateway = await uCGateway.Create(Guid.NewGuid().ToString(), "Gateway 10", "192.168.10.1", true, null);
                Assert.NotNull(gateway);
            }
        }

        [Fact]
        public async Task List()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCGateway uCGateway = new UCGateway(_unitOfWork, new EventHandler());
                await uCGateway.Create(Guid.NewGuid().ToString(), "Gateway", "192.168.10.1", true, null);
                await uCGateway.Create(Guid.NewGuid().ToString(), "Gate11", "192.168.10.2", true, null);
                await uCGateway.Create(Guid.NewGuid().ToString(), "Gateway 12", "192.168.10.3", true, null);
                List<Gateway> gateways = await uCGateway.List("gateway", 1, 2);
                Assert.Equal(2, gateways.Count);
            }
        }

        [Fact]
        public async Task GetById()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCGateway UCGateway = new UCGateway(_unitOfWork, new EventHandler());
                Gateway gateway = await UCGateway.Create(Guid.NewGuid().ToString(), "Gateway 12", "192.168.10.3", true, null);
                Gateway gateway1 = await UCGateway.GetById((int)gateway.Id);
                Assert.NotNull(gateway1);
            }
        }

        [Fact]
        public async Task Count()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCGateway uCGateway = new UCGateway(_unitOfWork, new EventHandler());
                await uCGateway.Create(Guid.NewGuid().ToString(), "Gateway 10", "192.168.10.1", true, null);
                await uCGateway.Create(Guid.NewGuid().ToString(), "Gateway 11", "192.168.10.2", true, null);
                await uCGateway.Create(Guid.NewGuid().ToString(), "Gateway 12", "192.168.10.3", true, null);
                int total = await uCGateway.Count();
                Assert.Equal(total, 3);
            }
        }

        [Fact]
        public async Task Delete()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCGateway UCGateway = new UCGateway(_unitOfWork, new EventHandler());
                Gateway gateway = await UCGateway.Create(Guid.NewGuid().ToString(), "Gateway 12", "192.168.10.3", true, null);
                bool response = await UCGateway.Delete((int)gateway.Id);
                Assert.NotNull(response);
            }
        }
    }
}
