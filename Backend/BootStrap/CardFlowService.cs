using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using Rhino.ServiceBus;
using Topshelf;
using Topshelf.Logging;

namespace Solyutor.CardFlow.Backend.BootStrap
{
    public class CardFlowService : ServiceControl
    {
        private WindsorContainer _windsor;

        public bool Start(HostControl hostControl)
        {
            
            ConfigureContainer();

            StartBus();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _windsor.Dispose();
            HostLogger.Shutdown(); //shall I care about it?
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
            _windsor.AddFacility<TypedFactoryFacility>();
            _windsor.Install(
                new ConsumersInstaller(),
                new EventStoreInstaller(),
                new ServiceBusInstaller());
        }
    }
}