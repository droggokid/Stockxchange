using StockManagementSystemClasses.Controller;
using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;
using StockManagementSystemClasses.Models;
using NUnit.Framework;
using NSubstitute;

namespace StockManagementSystemTest;

public class ShareTests
{
    private IShare uut;
    private IStockManager stockManager;
    private ITradeAdvisor tradeAdvisor;
    private IStockProvider stockProvider;

    [SetUp]
    public void Setup()
    {
        stockProvider = Substitute.For<IStockProvider>();
        tradeAdvisor = Substitute.For<ITradeAdvisor>();
        stockManager = new StockManager(stockProvider, tradeAdvisor);
        uut = new Share("TestShare", tradeAdvisor);
    }

    [Test]
    public void OnStockUpdate_ShouldCallTradeAdvisorUpdate()
    {
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        uut.OnStockUpdate(this, args);
        tradeAdvisor.Received().Update(uut);
    }

    [Test]
    public void OnStockUpdate_ShouldCallTradeAdvisorUpdateWithCorrectShare()
    {
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        uut.OnStockUpdate(this, args);
        tradeAdvisor.Received().Update(Arg.Is<IShare>(x => x.Name == "TestShare"));
    }

    [Test]
    public void OnStockUpdate_ShouldCallTradeAdvisorUpdateWithCorrectValues1()
    {
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        uut.OnStockUpdate(this, args);
        tradeAdvisor.Received().Update(Arg.Is<IShare>(x => x.GetValues(1).Item2 == 10));
    }

    [Test]
    public void OnStockUpdate_ShouldCallTradeAdvisorUpdateWithCorrectValues2()
    {
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        uut.OnStockUpdate(this, args);
        tradeAdvisor.Received().Update(Arg.Is<IShare>(x => x.GetValues(1).Item1 == args.Time));
    }

    [Test]
    public void StartSupervision_ShouldCallTradeAdvisorSetAdvisorStrategy1()
    {
        uut.StartSupervision(tradeAdvisor, "NoAdvisor", new float[] { 10, 20 });
        tradeAdvisor.Received().setAdvisorStrategy("NoAdvisor", Arg.Is<float[]>(x => x.SequenceEqual(new float[] { 10, 20 })));
    }

    [Test]
    public void StartSupervision_ShouldCallTradeAdvisorSetAdvisorStrategy2()
    {
        uut.StartSupervision(tradeAdvisor, "NoAdvisor", new float[] { 10, 20 });
        tradeAdvisor.Received().setAdvisorStrategy("NoAdvisor", Arg.Is<float[]>(x => x.Length == 2));
    }

    [Test]
    public void StartSupervision_ShouldCallTradeAdvisorSetAdvisorStrategy3()
    {
        uut.StartSupervision(tradeAdvisor, "NoAdvisor", new float[] { 10, 20 });
        tradeAdvisor.Received().setAdvisorStrategy(Arg.Is<string>("NoAdvisor"), Arg.Any<float[]>());
    }

    [Test]
    public void StartSupervision_ShouldCallTradeAdvisorSetAdvisorStrategy4()
    {
        uut.StartSupervision(tradeAdvisor, "LimitAdvisor", new float[] { 10, 20 });
        tradeAdvisor.Received().setAdvisorStrategy(Arg.Is<string>("LimitAdvisor"), Arg.Any<float[]>());
    }

    [Test]
    public void StartSupervision_ShouldCallTradeAdvisorSetAdvisorStrategy5()
    {
        uut.StartSupervision(tradeAdvisor, "RegressionAdvisor", new float[] { 10, 20 });
        tradeAdvisor.Received().setAdvisorStrategy(Arg.Is<string>("RegressionAdvisor"), Arg.Any<float[]>());
    }

    [Test]
    public void GetValues_ShouldReturnCorrectValues1()
    {
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 });
        var result = uut.GetValues(1);
        Assert.That(result.Item1, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromMilliseconds(10)));
        Assert.That(result.Item2, Is.EqualTo((float)10));
    }

    [Test]
    public void GetValues_ShouldReturnCorrectValues2()
    {
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 20 });
        var result = uut.GetValues(2);
        Assert.That(result.Item1, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromMilliseconds(10)));
        Assert.That(result.Item2, Is.EqualTo((float)10));
    }

    [Test]
    public void GetValues_ShouldReturnCorrectValues3()
    {
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 20 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 30 });
        var result = uut.GetValues(-1);
        Assert.That(result.Item1, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromMilliseconds(10)));
        Assert.That(result.Item2, Is.EqualTo((float)10));
    }

    [Test]
    public void GetValues_ShouldReturnCorrectValues4()
    {
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 20 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 30 });
        var result = uut.GetValues(40);
        Assert.That(result.Item1, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromMilliseconds(10)));
        Assert.That(result.Item2, Is.EqualTo((float)30));
    }
}