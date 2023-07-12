using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Entities
{
    public class AuditTrailManager
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        //public User? User { get; set; }
        /// <summary>
        /// Especifica el evento ocurrido. 
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Estado del evento. 
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// JSON con los datos a los que se les realizo la accion
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// Guarda el IP o nombre de la maquina que realizo la operacion
        /// </summary>
        public string? Host { get; set; }
        public DateTime Date { get; set; }
    }
}
