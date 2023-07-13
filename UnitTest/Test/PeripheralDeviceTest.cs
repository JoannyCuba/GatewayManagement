using GatewayManagementCore.Entities;
using GatewayManagementCore.UseCase;
using UnitTest.Infraestructure;
using Xunit;
using EventHandler = UnitTest.Infraestructure.EventHandler;

namespace UnitTest.Test
{
    public class PeripheralDeviceTest
    {
        [Fact]
        public async Task Top_Allowed_Device()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCPeripheralDevice ucPeripheralDevice = new UCPeripheralDevice(_unitOfWork, new EventHandler());
                await ucPeripheralDevice.Create(null, "Sony", true, true, 1);
                await ucPeripheralDevice.Create(null, "Xiaomi", false, true, 1);
                await ucPeripheralDevice.Create(null, "Samsung", false, true, 1);
                await ucPeripheralDevice.Create(null, "Sony", true, true, 1);
                await ucPeripheralDevice.Create(null, "Xiaomi", false, true, 1);
                await ucPeripheralDevice.Create(null, "Samsung", false, true, 1);
                await ucPeripheralDevice.Create(null, "Sony", true, true, 1);
                await ucPeripheralDevice.Create(null, "Xiaomi", false, true, 1);
                await ucPeripheralDevice.Create(null, "Apple", false, true, 1);
                await ucPeripheralDevice.Create(null, "K120", false, true, 1);
                await Assert.ThrowsAnyAsync<Exception>(() => ucPeripheralDevice.Create(null, "Winnia", false, true, 1));
            }
        }

        [Fact]
        public async Task Add_Correctly()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCPeripheralDevice ucPeripheralDevice = new UCPeripheralDevice(_unitOfWork, new EventHandler());
                PeripheralDevice device = await ucPeripheralDevice.Create(null,"Sony",true,true, null);
                Assert.NotNull(device);
            }
        }

        [Fact]
        public async Task List()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCPeripheralDevice ucPeripheralDevice = new UCPeripheralDevice(_unitOfWork, new EventHandler());
                await ucPeripheralDevice.Create(null, "Sony", true, true, null);
                await ucPeripheralDevice.Create(null, "Xiaomi", false, true, null);
                await ucPeripheralDevice.Create(null, "Samsung", false, true, null);
                List<PeripheralDevice> gateways = await ucPeripheralDevice.List("sony", 1, 2);
                Assert.Equal(1, gateways.Count);
            }
        }

        [Fact]
        public async Task GetById()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCPeripheralDevice UCPeripheralDevice = new UCPeripheralDevice(_unitOfWork, new EventHandler());
                PeripheralDevice device = await UCPeripheralDevice.Create(null, "Samsung", false, true, null);
                PeripheralDevice device1 = await UCPeripheralDevice.GetById((int)device.Id);
                Assert.NotNull(device1);
            }
        }

        [Fact]
        public async Task Count()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCPeripheralDevice ucPeripheralDevice = new UCPeripheralDevice(_unitOfWork, new EventHandler());
                await ucPeripheralDevice.Create(null, "Sony", true, true, null);
                await ucPeripheralDevice.Create(null, "Xiaomi", false, true, null);
                await ucPeripheralDevice.Create(null, "Samsung", false, true, null);
                int total = await ucPeripheralDevice.Count();
                Assert.Equal(total, 3);
            }
        }

        [Fact]
        public async Task Delete()
        {
            using (var _unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                UCPeripheralDevice UCPeripheralDevice = new UCPeripheralDevice(_unitOfWork, new EventHandler());
                PeripheralDevice device = await UCPeripheralDevice.Create(null, "Samsung", false, true, null);
                bool response = await UCPeripheralDevice.Delete((int)device.Id);
                Assert.NotNull(response);
            }
        }
    }
}
