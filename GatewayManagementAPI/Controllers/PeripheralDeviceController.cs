using AutoMapper;
using GatewayManagementAPI.Infraestructure.Dtos;
using GatewayManagementAPI.Utils;
using GatewayManagementCore.Entities;
using GatewayManagementCore.Interfaces;
using GatewayManagementCore.UseCase;
using log4net;
using Microsoft.AspNetCore.Mvc;
using static GatewayManagementAPI.Utils.Constants;

namespace GatewayManagementAPI.Controllers
{
    [Route("api/peripheralDevice")]
    [ApiController]
    public class PeripheralDeviceController : ControllerBase
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;
        private readonly IConfiguration Configuration;
        private readonly UCPeripheralDevice UCPeripheralDevice;
        public readonly ILog _logger = LogManager.GetLogger(typeof(PeripheralDeviceController));


        public PeripheralDeviceController(IUnitOfWork context, IEventHandler handler, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            Configuration = configuration;
            UCPeripheralDevice = new UCPeripheralDevice(context, handler);
        }
        [HttpGet]
        public async Task<ApiResult> List(string? filter = "", int page = 1, int elementsPerPage = 25)
        {
            try
            {
                List<PeripheralDevice> elements = await UCPeripheralDevice.List(filter, page, elementsPerPage);

                int total = await UCPeripheralDevice.Count(filter);

                return new ApiResult() { state = StatusResponse.Ok, data = new { elements, total } };
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return new ApiResult() { state = StatusResponse.Error, data = e.Message };
            }
        }
        [HttpGet("{id}")]
        public async Task<ApiResult> GetById(int id)
        {
            try
            {
                PeripheralDevice elemento = await UCPeripheralDevice.GetById(id);
                return new ApiResult() { state = StatusResponse.Ok, data = elemento };
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return new ApiResult() { state = StatusResponse.Error, data = e.Message };
            }
        }
        [HttpPost]
        public async Task<ApiResult> Post(PeripheralDeviceDto deviceDto)
        {
            try
            {
                await UCPeripheralDevice.Create(deviceDto.uId, deviceDto.vendor, deviceDto.isOnline, true, deviceDto.gatewayId);
                return new ApiResult() { state = StatusResponse.Ok, data = null };
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
                HttpContext.Response.StatusCode = 500;
                return new ApiResult() { state = StatusResponse.Error, data = e.Message };
            }
        }
        [HttpPut]
        public async Task<ApiResult> Put(PeripheralDeviceDto deviceDto)
        {
            try
            {
                await UCPeripheralDevice.Update((int)deviceDto.id, deviceDto.uId, deviceDto.vendor, deviceDto.isOnline, true, deviceDto.gatewayId);
                return new ApiResult() { state = StatusResponse.Ok, data = null };
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
                HttpContext.Response.StatusCode = 500;
                return new ApiResult() { state = StatusResponse.Error, data = e.Message };
            }
        }
        [HttpDelete]
        public async Task<ApiResult> Delete(int id)
        {
            try
            {
                bool response = await UCPeripheralDevice.Delete(id);
                return new ApiResult() { state = StatusResponse.Ok, data = null };
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return new ApiResult() { state = StatusResponse.Error, data = e.Message };
            }
        }
    }
}
