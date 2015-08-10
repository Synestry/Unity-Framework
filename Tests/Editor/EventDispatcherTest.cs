using Assets.Scripts.Framework.Event;

using NUnit.Framework;

namespace Assets.Scripts.Framework.Tests.Editor
{
    [TestFixture]
    [Category("Event Dispatcher Tests")]
    internal class EventDispatcherTest
    {
        private void OnRemoveHandlerCallback(GameEvent gameEvent)
        {
            Assert.Fail();
        }

        private void OnRemoveCustomHandlerCallback(TestEvent testEvent)
        {
            Assert.Fail();
        }

        private bool has1Dispatched;

        private bool has2Dispatched;

        [Test]
        public void AddCustomHandlerTest()
        {
            var eventDispatcher = new EventDispatcher();
            string testData = "testing";

            eventDispatcher.AddHandler<TestEvent>(TestEventType.TestEvent1, testEvent =>
            {
                if (testEvent.TestData == testData)
                {
                    Assert.Pass();
                }
            });

            eventDispatcher.Dispatch(TestEventType.TestEvent1, new TestEvent { TestData = testData });
            Assert.Fail();
        }

        [Test]
        public void AddEventHandlerTest()
        {
            var eventDispatcher = new EventDispatcher();
            eventDispatcher.AddHandler(TestEventType.TestEvent1, gameEvent => Assert.Pass());
            eventDispatcher.Dispatch(TestEventType.TestEvent1);
            Assert.Fail();
        }

        [Test]
        public void ParentAddHandlerTest()
        {
            has1Dispatched = false;
            has2Dispatched = false;

            var parent = new EventDispatcher();
            var child1 = new EventDispatcher(parent);
            var child2 = new EventDispatcher(parent);

            child1.AddHandler<TestEvent>(TestEventType.TestEvent1, testEvent => has1Dispatched = true);
            child2.AddHandler<TestEvent>(TestEventType.TestEvent1, testEvent => has2Dispatched = true);

            //Test dispatching from child
            child1.Dispatch(TestEventType.TestEvent1, new TestEvent());
            Assert.True(has1Dispatched);
            Assert.True(has2Dispatched);

            //Test dispatching from parent
            has1Dispatched = false;
            has2Dispatched = false;
            parent.Dispatch(TestEventType.TestEvent1, new TestEvent());
            Assert.True(has1Dispatched);
            Assert.True(has2Dispatched);
        }

        [Test]
        public void RemoveAllHandlersTest()
        {
            has1Dispatched = false;
            has2Dispatched = false;

            var parent = new EventDispatcher();
            var child = new EventDispatcher(parent);

            child.AddHandler<TestEvent>(TestEventType.TestEvent1, testEvent => has1Dispatched = true);
            child.AddHandler<TestEvent>(TestEventType.TestEvent2, testEvent => has1Dispatched = true);
            parent.AddHandler<TestEvent>(TestEventType.TestEvent2, testEvent => has2Dispatched = true);

            //Check handlers are removed from child
            child.RemoveAllHandlers();
            child.Dispatch(TestEventType.TestEvent1);
            Assert.False(has1Dispatched);

            //Check child hasn't removed all handlers from parent
            parent.Dispatch(TestEventType.TestEvent2);
            Assert.True(has2Dispatched);

            //check parent handler still gets called from child dispatcher
            has2Dispatched = false;
            child.Dispatch(TestEventType.TestEvent2);
            Assert.False(has1Dispatched);
            Assert.True(has2Dispatched);
        }

        [Test]
        public void RemoveCustomHandlerTest()
        {
            var eventDispatcher = new EventDispatcher();
            eventDispatcher.AddHandler<TestEvent>(TestEventType.TestEvent1, OnRemoveCustomHandlerCallback);
            eventDispatcher.RemoveHandler<TestEvent>(TestEventType.TestEvent1, OnRemoveCustomHandlerCallback);
            eventDispatcher.Dispatch(TestEventType.TestEvent1, new TestEvent());
            Assert.Pass();
        }

        [Test]
        public void RemoveEventHandlerTest()
        {
            var eventDispatcher = new EventDispatcher();
            eventDispatcher.AddHandler(TestEventType.TestEvent1, OnRemoveHandlerCallback);
            eventDispatcher.RemoveHandler(TestEventType.TestEvent1, OnRemoveHandlerCallback);
            eventDispatcher.Dispatch(TestEventType.TestEvent1);
            Assert.Pass();
        }

        [Test]
        public void StopPropagationTest()
        {
            has1Dispatched = false;
            has2Dispatched = false;

            var parent = new EventDispatcher();
            var child1 = new EventDispatcher(parent);
            var child2 = new EventDispatcher(parent);

            child1.AddHandler<TestEvent>(TestEventType.TestEvent1, testEvent =>
            {
                has1Dispatched = true;
                testEvent.StopPropagation = true;
            });

            child2.AddHandler<TestEvent>(TestEventType.TestEvent1, testEvent => has2Dispatched = true);

            child1.Dispatch(TestEventType.TestEvent1, new TestEvent());
            Assert.IsTrue(has1Dispatched);
            Assert.IsFalse(has2Dispatched);
        }
    }

    internal enum TestEventType
    {
        TestEvent1,

        TestEvent2
    }

    internal class TestEvent : GameEvent
    {
        public string TestData { get; set; }
    }
}