using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Coorth.Tests.Events {

    
    public class EventDispatchTest {

        private EventDispatcher root;

        [SetUp]
        public void Setup() {
            root = new EventDispatcher();
        }

        [Test]
        public void DispatchEventSync() {
            var isTrigger = false;
            var value = 0;
            root.Subscribe<EventTest>((in EventTest e) => {
                isTrigger = true;
                value = e.Value;
            });
            
            root.Dispatch(new EventTest(){Value = 17});
            Assert.IsTrue(isTrigger);
            Assert.IsTrue(value == 17);
            
            root.Dispatch(new EventTest(){Value = 21});
            Assert.IsTrue(value == 21);
        }

        [Test]
        public void DispatchEventAsync() {
            var isTrigger = false;
            var value = 0;
            root.Subscribe<EventTest>((in EventTest e) => {
                isTrigger = true;
                value = e.Value;
            });
            root.DispatchAsync(new EventTest(){Value = 17});
            Assert.IsTrue(isTrigger);
            Assert.IsTrue(value == 17);
        }

        [Test]
        public async Task DispatchEventAsync2() {
            var isTrigger = false;
            var value = 0;
            root.Subscribe<EventTest>((Func<EventTest, Task>)(async e => {
                isTrigger = true;
                value = e.Value;
                await Task.CompletedTask;
            }));
            await root.DispatchAsync(new EventTest(){Value = 17});
            Assert.IsTrue(isTrigger);
            Assert.IsTrue(value == 17);
        }
    }
}

