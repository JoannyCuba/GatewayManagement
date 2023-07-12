using GatewayManagementCore.Interfaces;
using GatewayManagementCore.Utils;
using log4net;
using System.Text.Json;

namespace GatewayManagementAPI.Infraestructure
{
    public class EventHandler : IEventHandler
    {
        public readonly ILog _logger = LogManager.GetLogger(typeof(EventHandler));
        public void ThrowEvent(string @event, params object[] arguments)
        {
            Console.Write("Event: ", @event, arguments);
            switch (@event)
            {
                case Constants.Events.UnexpectedError:
                    {
                        Task.Run(delegate
                        {
                            _logger.Warn(arguments);
                            _logger.ErrorFormat("Fatal error. {0}. {1}", arguments);
                        });
                    }
                    break;
                default:
                    {
                        _logger.InfoFormat("Event : {0}. {1}", @event, JsonSerializer.Serialize(arguments));
                    }
                    break;
            }
        }
    }
}
