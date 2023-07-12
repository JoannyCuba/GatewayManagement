using GatewayManagementCore.Entities;
using GatewayManagementCore.Interfaces;
using GatewayManagementCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.UseCase
{
    public class UCPeripheralDevice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventHandler _eventHandler;
        public UCPeripheralDevice(IUnitOfWork unitOfWork, IEventHandler eventHandler)
        {
            _unitOfWork = unitOfWork;
            _eventHandler = eventHandler;
        }

        public async Task<List<PeripheralDevice>> List(string filter = "", int page = 1, int itemsPerPage = 25)
        {
            try
            {
                List<PeripheralDevice> devices = await _unitOfWork.PeripheralDevice.FindPaginationAsync(
                page: page,
                itemPerPage: itemsPerPage,
                filter: x => (string.IsNullOrEmpty(filter) || x.DateCreated.ToString() == filter || x.Id.ToString() == filter) && x.IsActive == true);
                _eventHandler.ThrowEvent(Constants.Events.ListPeripheralDevices, devices);
                return devices;
            }
            catch (Exception e)
            {
                _eventHandler.ThrowEvent(Constants.Events.UnexpectedError, this.GetType().Name, e.Message, e.StackTrace);
                throw new Exception(Constants.GeneralErrors.UnexpectedError);
            }
        }
        public async Task<int> Count(string filtro = "")
        {
            return await _unitOfWork.PeripheralDevice.CountAsync(
                filter: x => (x.IsActive == true) && (string.IsNullOrEmpty(filtro) || x.DateCreated.ToString().Contains(filtro.ToLower()) || x.UID.ToString().Contains(filtro.ToLower()) || x.Vendor.ToLower().Contains(filtro.ToLower()))
            );
        }
        public async Task<PeripheralDevice> GetById(int id)
        {
            try
            {
                if (id == null)
                    throw new Exception("The id cannot be null");
                PeripheralDevice device = (PeripheralDevice)await _unitOfWork.PeripheralDevice.FindOneAsync(filter: x => x.Id == id) ??
                    throw new Exception("Peripheral Device Not Found");
                _eventHandler.ThrowEvent(Constants.Events.GetPeripheralDeviceById, device);
                return device;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<PeripheralDevice> Create(int uid, string vendor, bool isOnline, bool IsActive, int? gatewayId)
        {
            try
            {
                if (uid == null)
                    throw new Exception("The field UID cannot be null");
                if (gatewayId != null)
                {
                    int elements = await _unitOfWork.PeripheralDevice.CountAsync(x => x.GatewayId == gatewayId);
                    if (elements > 10)
                    {
                        throw new Exception("Cannot add a new peripheral device to the selected gateway as it has reached the allowed limit of devices.");
                    }
                }
                PeripheralDevice device = new()
                {
                    UID = uid,
                    Vendor = vendor,
                    IsActive = IsActive,
                    IsOnline = isOnline,
                    DateCreated = DateTime.Now,
                    GatewayId = gatewayId
                };
                _unitOfWork.PeripheralDevice.Add(device);
                await _unitOfWork.Save();
                _eventHandler.ThrowEvent(Constants.Events.CreateGateway, device);
                return device;
            }
            catch (Exception e)
            {
                _eventHandler.ThrowEvent(Constants.Events.UnexpectedError, this.GetType().Name, e.Message, e.StackTrace);
                throw new Exception(Constants.GeneralErrors.UnexpectedError);
            }
        }

        public async Task<PeripheralDevice> Update(int id, int uid, string vendor, bool isOnline, bool IsActive, int? gatewayId)
        {
            try
            {

                if (uid == null)
                    throw new Exception("The field UID cannot be null");
                if (gatewayId != null)
                {
                    int elements = await _unitOfWork.PeripheralDevice.CountAsync(x => x.GatewayId == gatewayId);
                    if (elements > 10)
                    {
                        throw new Exception("Cannot update the peripheral device to the selected gateway as it has reached the allowed limit of devices.");
                    }
                }
                PeripheralDevice device = new()
                {
                    Id = id,
                    UID = uid,
                    Vendor = vendor,
                    IsActive = IsActive,
                    IsOnline = isOnline,
                    DateCreated = DateTime.Now,
                    GatewayId = gatewayId
                };
                _unitOfWork.PeripheralDevice.Update(device);
                await _unitOfWork.Save();
                _eventHandler.ThrowEvent(Constants.Events.CreateGateway, device);
                return device;
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
            _unitOfWork.PeripheralDevice.Delete(filter: x => x.Id == id);
            await _unitOfWork.Save();
            return true;
        }

    }
}
