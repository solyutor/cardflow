using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Logging.NLog;

namespace Solyutor.CardFlow.Backend.BootStrap
{
    public class EventStoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDispatchCommits>()
                         .ImplementedBy<ServiceBusDispatcher>()
                         .LifestyleSingleton(),

                Component.For<IStoreEvents>()
                         .UsingFactoryMethod(BuildEventStore)
                         .LifestyleSingleton());
        }

        private static IStoreEvents BuildEventStore(IKernel kernel)
        {
            var dispatcher = kernel.Resolve<IDispatchCommits>();

            return Wireup.Init()
                         .LogTo(type => new NLogLogger(type))
                         .UsingInMemoryPersistence()
//                .UsingJsonSerialization()
                         .InitializeStorageEngine()
                         .UsingSynchronousDispatchScheduler()
                         .DispatchTo(dispatcher)
                         .Build();
        }
    }
}