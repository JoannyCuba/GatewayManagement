using GatewayManagementCore.Entities;
using GatewayManagementCore.Interfaces;
using GatewayManagementCore.Utils;
using System.Net;
using Constants = GatewayManagementCore.Utils.Constants;

namespace GatewayManagementCore.UseCase
{
    public class UCGateway
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventHandler _eventHandler;

        public UCGateway(IUnitOfWork unitOfWork, IEventHandler eventHandler)
        {
            _unitOfWork = unitOfWork;
            _eventHandler = eventHandler;
        }
        public async Task<List<Gateway>> List(string filter = "", int page = 1, int itemsPerPage = 25)
        {
            try
            {
                List<Gateway> gateways = await _unitOfWork.Gateway.FindPaginationAsync(
                page: page,
                itemPerPage: itemsPerPage,
                filter: x => (string.IsNullOrEmpty(filter) || x.Name.ToLower().Contains(filter) || x.Id.ToString() == filter) && x.IsActive == true);
                _eventHandler.ThrowEvent(Constants.Events.ListGateways, gateways);
                return gateways;
            }
            catch (Exception e)
            {
                _eventHandler.ThrowEvent(Constants.Events.UnexpectedError, this.GetType().Name, e.Message, e.StackTrace);
                throw new Exception(Constants.GeneralErrors.UnexpectedError);
            }
        }
        public async Task<int> Count(string filtro = "")
        {
            return await _unitOfWork.Gateway.CountAsync(
                filter: x => (x.IsActive == true) && (string.IsNullOrEmpty(filtro) || x.Name.ToLower().Contains(filtro.ToLower()) || x.SerialNumber.ToLower().Contains(filtro.ToLower()) || x.IPAddress.ToLower().Contains(filtro.ToLower()))
            );
        }
        public async Task<Gateway> GetById(int id)
        {
            try
            {
                if (id == null)
                    throw new Exception("The id cannot be null");
                Gateway gateway = (Gateway)await _unitOfWork.Gateway.FindOneAsync(filter: x => x.Id == id) ??
                    throw new Exception("Gateway Not Found");
                _eventHandler.ThrowEvent(Constants.Events.GetGatewayById, gateway);
                return gateway;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Gateway> Create(string serial, string name, string ipAddress, bool IsActive, List<PeripheralDevice> devices)
        {
            try
            {
                if (string.IsNullOrEmpty(serial))
                    throw new Exception("The field serial number cannot be null or empty");
                if (string.IsNullOrEmpty(ipAddress))
                    throw new Exception("The field ipAddress cannot be null or empty");
                else
                {
                    if (!IPValidationService.ValidateIPAddress(ipAddress))
                    {
                        throw new Exception("The field Ip Address is not valid.");
                    }
                }
                Gateway gateway = new()
                {
                    SerialNumber = serial,
                    Name = name,
                    IPAddress = ipAddress,
                    IsActive = IsActive,
                    PeripheralDevices = devices
                };
                _unitOfWork.Gateway.Add(gateway);
                await _unitOfWork.Save();
                _eventHandler.ThrowEvent(Constants.Events.CreateGateway, gateway);
                return gateway;
            }
            catch (Exception e)
            {
                _eventHandler.ThrowEvent(Constants.Events.UnexpectedError, this.GetType().Name, e.Message, e.StackTrace);
                throw new Exception(Constants.GeneralErrors.UnexpectedError);
            }
        }

        public async Task<Gateway> Update(int id, string serial, string name, string ipAddress, bool IsActive, List<PeripheralDevice> devices)
        {
            try
            {
                if (id == null)
                    throw new Exception("The id cannot be null");
                if (string.IsNullOrEmpty(serial))
                    throw new Exception("The field serial number cannot be null or empty");
                if (string.IsNullOrEmpty(ipAddress))
                    throw new Exception("The field ipAddress cannot be null or empty");
                else
                {
                    if (!IPValidationService.ValidateIPAddress(ipAddress))
                    {
                        throw new Exception("The email is not a valid email format");
                    }
                }
                Gateway gateway = new()
                {
                    Id = id,
                    SerialNumber = serial,
                    Name = name,
                    IPAddress = ipAddress,
                    IsActive = IsActive,
                    PeripheralDevices = devices
                };
                _unitOfWork.Gateway.Update(gateway);
                await _unitOfWork.Save();

                _eventHandler.ThrowEvent(Constants.Events.UpdateGateway, gateway);
                return gateway;
            }
            catch (Exception e)
            {
                _eventHandler.ThrowEvent(Constants.Events.UnexpectedError, this.GetType().Name, e.Message, e.StackTrace);
                throw new Exception(Constants.GeneralErrors.UnexpectedError);
            }
        }
        public async Task<bool> Delete(int id)
        {
            if (id == null)
                throw new Exception("id field cannot be null");
            _unitOfWork.Gateway.Delete(filter: x => x.Id == id);
            await _unitOfWork.Save();
            return true;
        }

    }
}
