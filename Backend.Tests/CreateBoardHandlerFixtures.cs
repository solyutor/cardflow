using System.Linq;
using EventStore;
using FluentAssertions;
using NUnit.Framework;
using Solyutor.CardFlow.Backend.BoardManagement;
using Solyutor.CardFlow.Messages.BoardManagement;

namespace Solyutor.CardFlow.Backend.Tests
{
    [TestFixture]
    public class CreateBoardHandlerFixtures
    {
        [SetUp]
        public void Setup()
        {
            _eventStore = Wireup.Init()
                                .UsingInMemoryPersistence()
                                .InitializeStorageEngine()
                                .UsingSynchronousDispatchScheduler()
                                .Build();
        }

        [TearDown]
        public void TearDown()
        {
            _eventStore.Dispose();
        }

        private IStoreEvents _eventStore;

        [Test]
        //TODO This is an ugly way of testing. It obvious due to word 'and' in its method. Such style of testing should be avoided by splitting it into separate test, each on asserts a aspect of system. Tell about in a blog.
        public void Should_save_event_to_stream_and_notify_via_bus()
        {
            var bus = new FakeServiceBus();

            var consumer = new CreateBoardHandler(bus, _eventStore);

            var createBoardCommand = new CreateBoardCommand
                                         {
                                             Name = "Kanban",
                                             States = new[]
                                                          {
                                                              new State
                                                                  {
                                                                      Name = "Ready",
                                                                      Order = 1,
                                                                      Capacity = 1
                                                                  }
                                                          }
                                         }
                ;

            consumer.Consume(createBoardCommand);

            var boardCreatedEvent = bus.Notified.Last<BoardCreatedEvent>();
            boardCreatedEvent
                .ShouldHave()
                .SharedProperties()
                .EqualTo(createBoardCommand);

            var eventFromEventStore = _eventStore
                .OpenStream(boardCreatedEvent.Id, 0, int.MaxValue)
                .CommittedEvents
                .Last();
            
            eventFromEventStore
                .ShouldHave()
                .SharedProperties()
                .EqualTo(boardCreatedEvent);
        }
    }
}