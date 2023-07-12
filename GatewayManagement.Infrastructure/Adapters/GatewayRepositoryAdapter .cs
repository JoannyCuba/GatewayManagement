using GatewayManagement.Core.Interfaces;
using GatewayManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagement.Infrastructure.Adapters
{
    public class GatewayRepositoryAdapter : IGatewayRepository
    {
        private readonly IGatewayRepository repository;

        public GatewayRepositoryAdapter(IGatewayRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddGateway(Gateway gateway)
        {
            // Convertir entidad de dominio en entidad de datos si es necesario
            await repository.AddGateway(gateway);
        }

        public async Task<Gateway> GetGateway(string serialNumber)
        {
            // Obtener entidad de datos y convertirla en entidad de dominio si es necesario
            return await repository.GetGateway(serialNumber);
        }

        public async Task<IEnumerable<Gateway>> GetAllGateways()
        {
            // Obtener entidades de datos y convertirlas en entidades de dominio si es necesario
            return await repository.GetAllGateways();
        }

        public async Task RemoveGateway(string serialNumber)
        {
            await repository.RemoveGateway(serialNumber);
        }
    }

}
