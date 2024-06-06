using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;
using StockManagementSystemClasses.Models;
using NUnit.Framework;
using NSubstitute;
using System.Reflection;

namespace StockManagementSystemTest;

public class ShareTests
{
    private Share uut;
    private Share uut1;
    private ITradeAdvisor LimitAdvisor;
    private ITradeAdvisor RegressionAdvisor;

    [SetUp]
    public void Setup()
    {
        LimitAdvisor = Substitute.For<ITradeAdvisor>();
        RegressionAdvisor = Substitute.For<ITradeAdvisor>();
        uut = new Share("TestShare", LimitAdvisor);
        uut1 = new Share("TestShare1", RegressionAdvisor);
    }

    [Test]
    public void OnStockUpdate_ShouldCallTradeAdvisorUpdate()
    {
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        uut.OnStockUpdate(this, args);
        LimitAdvisor.Received().Update(uut);
    }

    [Test]
    public void OnStockUpdate_ShouldCallTradeAdvisorUpdateWithCorrectShare()
    {
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        uut.OnStockUpdate(this, args);
        LimitAdvisor.Received().Update(Arg.Is<Share>(x => x.Name == "TestShare"));
    }

    [Test]
    public void OnStockUpdate_ShouldCallTradeAdvisorUpdateWithCorrectValues1()
    {
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        uut.OnStockUpdate(this, args);
        LimitAdvisor.Received().Update(Arg.Is<Share>(x => x.GetValues(1)[0].Item2 == 10));
    }

    [Test]
    public void OnStockUpdate_ShouldCallTradeAdvisorUpdateWithCorrectValues2()
    {
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        uut.OnStockUpdate(this, args);
        LimitAdvisor.Received().Update(Arg.Is<Share>(x => x.GetValues(1)[0].Item1 == args.Time));
    }

    /*[Test]
    public void StartSupervision_ShouldCallTradeAdvisorSetAdvisorStrategy1()
    {
        uut.StartSupervision(RegressionAdvisor);
        FieldInfo tradeAdvisorField = typeof(Share).GetField("_tradeAdvisor", BindingFlags.NonPublic | BindingFlags.Instance);
        var tradeAdvisorValue = tradeAdvisorField.GetValue(uut);

        Assert.That(tradeAdvisorValue, Is.EqualTo(RegressionAdvisor));
    }

    [Test]
    public void StartSupervision_ShouldCallTradeAdvisorSetAdvisorStrategy2()
    {
        uut.StartSupervision(LimitAdvisor);
        FieldInfo tradeAdvisorField = typeof(Share).GetField("_tradeAdvisor", BindingFlags.NonPublic | BindingFlags.Instance);
        var tradeAdvisorValue = tradeAdvisorField.GetValue(uut);

        Assert.That(tradeAdvisorValue, Is.EqualTo(LimitAdvisor));
    }*/

    [Test]
    public void GetValues_ShouldReturnCorrectValues1()
    {
        var fixedTime = new DateTime(2024, 6, 6, 19, 4, 26);
        uut.OnStockUpdate(this, new StockUpdateEventArgs { ShareName = "asd", Time = fixedTime, Value = 10 });
        var result = uut.GetValues(1);
        Assert.That(result[0].Item1, Is.EqualTo(fixedTime));
        Assert.That(result[0].Item2, Is.EqualTo((float)10));
    }

    [Test]
    public void GetValues_ShouldReturnCorrectValues2()
    {
        var fixedTime = new DateTime(2024, 6, 6, 19, 4, 26);
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = fixedTime, Value = 10 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = fixedTime, Value = 20 });
        var result = uut.GetValues(2);
        Assert.That(result[0].Item1, Is.EqualTo(fixedTime));
        Assert.That(result[0].Item2, Is.EqualTo((float)10));
    }

    [Test]
    public void GetValues_ShouldReturnCorrectValues3()
    {
        var fixedTime = new DateTime(2024, 6, 6, 19, 4, 26);
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = fixedTime, Value = 10 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = fixedTime, Value = 20 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = fixedTime, Value = 30 });
        var result = uut.GetValues(-1);
        Assert.That(result[0].Item1, Is.EqualTo(fixedTime));
        Assert.That(result[0].Item2, Is.EqualTo((float)10));
    }

    [Test]
    public void GetValues_ShouldReturnCorrectValues4()
    {
        var fixedTime = new DateTime(2024, 6, 6, 19, 4, 26);
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = fixedTime, Value = 10 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = fixedTime, Value = 20 });
        uut.OnStockUpdate(this, new StockUpdateEventArgs { Time = fixedTime, Value = 30 });
        var result = uut.GetValues(40);
        Assert.That(result[0].Item1, Is.EqualTo(fixedTime));
        Assert.That(result[0].Item2, Is.EqualTo((float)30));
    }
}