using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Castle.Windsor;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Solyutor.CardFlow.Messages.BoardManagement;
using TechTalk.SpecFlow;

namespace Solyutor.CardFlow.Backend.Tests
{
    [Binding]
    public class BoardManagementFeatureStep
    {
        private WindsorContainer _windsor;
        private IStartableServiceBus _bus;
        private CreateBoardCommand _message;
        private DirectoryInfo _testDirectory;
        private DirectoryInfo _serviceDirectory;
        private Process _service;

        public DirectoryInfo TestDirectory
        {
            get { return _testDirectory ?? (_testDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)); }
        }

        public DirectoryInfo ServiceDirectory
        {
            get { return _serviceDirectory ?? (_serviceDirectory = TestDirectory.Parent); }
        }

        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        [BeforeScenario]
        public void BeforeScenario()
        {
            CleanUpQueues();
            StartService();
            PrepareBus();
            PrepareScenario();

            //TODO: implement logic that has to run before executing each scenario
        }

        private void CleanUpQueues()
        {
            Action<DirectoryInfo> deleteEsentFrom = directory =>
                {
                    const bool recursively = true;
                    foreach (var subDirectory in directory.EnumerateDirectories().Where(subDirectory => subDirectory.Name.EndsWith(".esent")))
                    {
                        subDirectory.Delete(recursively);
                    }
                };

            deleteEsentFrom(TestDirectory);
            deleteEsentFrom(ServiceDirectory);
        }

        private void StartService()
        {
            var startInfo = new ProcessStartInfo(Path.Combine(ServiceDirectory.FullName,"Solyutor.CardFlow.Backend.exe"))
                {WorkingDirectory = ServiceDirectory.FullName};
            _service = Process.Start(startInfo);
        }

        private void PrepareScenario()
        {
            
        }

        private void PrepareBus()
        {
            _windsor = new WindsorContainer();
            new RhinoServiceBusConfiguration()
                .UseCastleWindsor(_windsor)
                .Configure();
            _bus = _windsor.Resolve<IStartableServiceBus>();
            _bus.Start();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Thread.Sleep(10000);
            _service.Kill();
            _service.WaitForExit(5000);
            _windsor.Dispose();
            CleanUpQueues();
        }

        [Given("I created new board named (.*)")]
        public void CreateNewBoard(string boardName)
        {
            _message = new CreateBoardCommand {Name = boardName};
        }

        [Given("using following parameters:")]
        public void SetBoardParameters(Table table)
        {
            _message.States =  table.Rows.Select(row => 
                new State
                {
                    Name = row["stepname"], 
                    Order = Convert.ToByte(row["order"]), 
                    Capacity = Convert.ToByte(row["capacity"])
                })
                .ToArray();
        }

        [When("I save changes")]
        public void WhenIPressAdd()
        {
            _bus.Send(_message);
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string result)
        {
            //TODO: implement assert (verification) logic
        }
    }
}
