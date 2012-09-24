using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.Windsor;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Config;
using Rhino.ServiceBus.Impl;
using Topshelf;

namespace Solyutor.CardFlow.Backend.BootStrap
{
    public class CardFlowService : ServiceControl
    {
        private WindsorContainer _windsor;
        private ILogger _logger;

        public bool Start(HostControl hostControl)
        {
            _windsor = new WindsorContainer();
            _windsor.AddFacility<LoggingFacility>(facility => facility.LogUsing(LoggerImplementation.NLog));

            ConfigureServiceBus();

            _logger = _windsor.Resolve<ILoggerFactory>().Create(GetType());
            _logger.Info("Service started");
            var bus = _windsor.Resolve<IStartableServiceBus>();

            bus.Start();
            

            return true;
        }

        private void ConfigureServiceBus()
        {
            new RhinoServiceBusConfiguration()
                .UseCastleWindsor(_windsor) //can be any of the containers supported
                .Configure();
        }

        public bool Stop(HostControl hostControl)
        {
            _logger.Info("Service stopped");
            _windsor.Dispose();
            return true;
        }
    }
}