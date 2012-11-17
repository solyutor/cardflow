using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Castle.Windsor;
using NUnit.Framework;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Solyutor.CardFlow.Messages.BoardManagement;
using TechTalk.SpecFlow;

namespace Solyutor.CardFlow.Backend.Tests
{
    [Binding]
    public class BoardManagementFeatureStep : ConsumerOf<BoardCreatedEvent>
    {
        private WindsorContainer _windsor;
        private IStartableServiceBus _bus;
        private CreateBoardCommand _message;
        private DirectoryInfo _testDirectory;
        private DirectoryInfo _serviceDirectory;
        private Process _service;
        private AutoResetEvent _responseReceived;

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
            _responseReceived = new AutoResetEvent(false);
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
            var pathToService = Path.Combine(ServiceDirectory.FullName, "Solyutor.CardFlow.Backend.exe");
            var startInfo = new ProcessStartInfo
                {
                    FileName = pathToService,
                    WorkingDirectory = ServiceDirectory.FullName
                };
            _service = new Process{StartInfo = startInfo};
            ThreadPool.QueueUserWorkItem(state => _service.Start());
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

        private void PrepareScenario()
        {
            _bus.AddInstanceSubscription(this);
        }
        
        [AfterScenario]
        public void AfterScenario()
        {
            _service.Kill();
            _service.WaitForExit(5000);
            _windsor.Dispose();
        }

        [Given]
        public void I_created_new_board_named_BOARDNAME_with_parameters(string boardName, Table table)
        {
            _message = new CreateBoardCommand
            {
                Name = boardName,
                States = table.Rows.Select(row =>
                                           new State
                                           {
                                               Name = row["stepname"],
                                               Order = Convert.ToByte(row["order"]),
                                               Capacity = Convert.ToByte(row["capacity"])
                                           })
                    .ToArray()
            };
        }

        [When]
        public void I_save_changes()
        {
            _bus.Send(_message);
        }

        [Then]
        public void I_should_receive_board_created_event()
        {
            var responseReceived = _responseReceived.WaitOne(TimeSpan.FromSeconds(5));
            Assert.That(responseReceived, Is.True);
        }

        public void Consume(BoardCreatedEvent message)
        {
            _responseReceived.Set();
        }
    }
}
