using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Castle;
using Rhino.ServiceBus.Impl;
using Solyutor.CardFlow.Backend.BoardManagement;
using Solyutor.CardFlow.Messages.BoardManagement;
using Topshelf;

namespace Solyutor.CardFlow.Backend.BootStrap
{
    public class CardFlowService : ServiceControl
    {
        private WindsorContainer _windsor;

        public bool Start(HostControl hostControl)
        {
            ConfigureContainer();

            ConfigureServiceBus();

            StartBus();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _windsor.Dispose();
            Topshelf.Logging.HostLogger.Shutdown();
            return true;
        }

        private void StartBus()
        {
            var bus = _windsor.Resolve<IStartableServiceBus>();

            bus.Start();
        }

        private void ConfigureContainer()
        {
            _windsor = new WindsorContainer();
            _windsor.AddFacility<LoggingFacility>(facility => facility.LogUsing(LoggerImplementation.NLog));

            _windsor.Register(Component
                                  .For<ConsumerOf<CreateBoardCommand>>()
                                  .ImplementedBy<CreateBoaradHandler>()
                                  .LifestyleTransient());

        }

        private void ConfigureServiceBus()
        {
            new RhinoServiceBusConfiguration()
                .UseCastleWindsor(_windsor) 
                .Configure();
        }
    }
}