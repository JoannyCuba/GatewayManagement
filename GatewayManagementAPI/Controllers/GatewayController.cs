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
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;
        private readonly IConfiguration Configuration;
        private readonly UCGateway UCGateway;
        public readonly ILog _logger = LogManager.GetLogger(typeof(GatewayController));


        public GatewayController(IUnitOfWork context, IEventHandler handler, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            Configuration = configuration;
            UCGateway = new UCGateway(context, handler);
        }

        [HttpGet]
        public async Task<ApiResult> List(string? filter = "", int page = 1, int elementsPerPage = 25)
        {
            try
            {
                List<Gateway> elements = await UCGateway.List(filter, page, elementsPerPage);
                int total = await UCGateway.Count(filter);

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
                Gateway elemento = await UCGateway.GetById(id);
                return new ApiResult() { state = StatusResponse.Ok, data = elemento };
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return new ApiResult() { state = StatusResponse.Error, data = e.Message };
            }
        }
        [HttpPost]
        public async Task<ApiResult> Post(GatewayDto gatewayDto)
        {
            try
            {
                List<PeripheralDevice> devices = mapper.Map<List<PeripheralDevice>>(gatewayDto.peripheralDevices);
                await UCGateway.Create(gatewayDto.serialNumber,gatewayDto.name,gatewayDto.ipAddress,true, devices);
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
        public async Task<ApiResult> Put(GatewayDto gatewayDto)
        {
            try
            {
                List<PeripheralDevice> devices = mapper.Map<List<PeripheralDevice>>(gatewayDto.peripheralDevices);
                await UCGateway.Update((int)gatewayDto.id, gatewayDto.serialNumber,gatewayDto.name,gatewayDto.ipAddress,true, devices);
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
                bool response = await UCGateway.Delete(id);
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
