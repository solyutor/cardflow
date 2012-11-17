using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EventStore;
using EventStore.Logging.NLog;

namespace Solyutor.CardFlow.Backend.BootStrap
{
    public class EventStoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IStoreEvents>()
                         .UsingFactoryMethod(BuildEventStore)
                );
        }

        private static IStoreEvents BuildEventStore()
        {
            return Wireup.Init()
                         .LogTo(type => new NLogLogger(type))
                         .UsingInMemoryPersistence()
//                .UsingJsonSerialization()
                         .InitializeStorageEngine()
                         .UsingSynchronousDispatchScheduler()
                         .Build();
        }
    }
}