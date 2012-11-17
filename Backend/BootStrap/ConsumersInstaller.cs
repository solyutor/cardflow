using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Rhino.ServiceBus;
using Solyutor.CardFlow.Backend.BoardManagement;
using Solyutor.CardFlow.Messages.BoardManagement;

namespace Solyutor.CardFlow.Backend.BootStrap
{
    public class ConsumersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component
                                  .For<ConsumerOf<CreateBoardCommand>>()
                                  .ImplementedBy<CreateBoardHandler>()
                                  .LifestyleTransient());
        }
    }
}