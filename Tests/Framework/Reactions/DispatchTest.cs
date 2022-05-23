using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Coorth.Framework; 

public class DispatchTest {

    private static Dispatcher Root => Dispatcher.Root;

    [SetUp]
    public void Setup() {
    }

    [Test]
    public void DispatchEventSync() {
        var isTrigger = false;
        var value = 0;
        Root.Subscribe((in EventTest e) => {
            isTrigger = true;
            value = e.Value;
        });
            
        Root.Dispatch(new EventTest(){Value = 17});
        Assert.IsTrue(isTrigger);
        Assert.IsTrue(value == 17);
            
        Root.Dispatch(new EventTest(){Value = 21});
        Assert.IsTrue(value == 21);
    }

    [Test]
    public void DispatchEventAsync() {
        var isTrigger = false;
        var value = 0;
        Root.Subscribe((in EventTest e) => {
            isTrigger = true;
            value = e.Value;
        });
        Root.DispatchAsync(new EventTest(){Value = 17});
        Assert.IsTrue(isTrigger);
        Assert.IsTrue(value == 17);
    }

    [Test]
    public async Task DispatchEventAsync2() {
        var isTrigger = false;
        var value = 0;
        Root.Subscribe((Func<EventTest, Task>)(async e => {
            isTrigger = true;
            value = e.Value;
            await Task.CompletedTask;
        }));
        await Root.DispatchAsync(new EventTest(){Value = 17});
        Assert.IsTrue(isTrigger);
        Assert.IsTrue(value == 17);
    }
}