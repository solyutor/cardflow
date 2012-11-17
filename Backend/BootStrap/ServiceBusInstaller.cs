using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;

namespace Solyutor.CardFlow.Backend.BootStrap
{
    public class ServiceBusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            new RhinoServiceBusConfiguration()
                .UseCastleWindsor(container)
                .Configure();
        }
    }
}