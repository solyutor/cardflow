using Solyutor.CardFlow.Backend.BootStrap;
using Topshelf;

namespace Solyutor.CardFlow.Backend
{
    public static class Program
    {
        public static void Main()
        {
            HostFactory.Run(host =>
                {
                    host.UseNLog();
                    host.Service<CardFlowService>();
                }
                );

            
        }
    }
}