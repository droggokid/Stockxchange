using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;
using StockManagementSystemClasses.Models;
using NUnit.Framework;
using NSubstitute;
using System.Reflection;

namespace StockManagementSystemTest;

public class TradeAdvisorTests
{
    private ITradeAdvisor uut;
    private Share share;


    [SetUp]
    public void Setup()
    {
        uut = new LimitAdvisor(0, 0);
        share = new Share("TestShare", uut);
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        share.OnStockUpdate(this, args);
    }

    [Test]
    public void Update_ShouldReturnBuyLimit()
    {
        uut = new LimitAdvisor(15, 20);
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Buy"));
    }

    [Test]
    public void Update_ShouldReturnKeepLimit()
    {
        uut = new LimitAdvisor(10, 20);
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Keep"));
    }

    [Test]
    public void Update_ShouldReturnSellLimit()
    {
        uut = new LimitAdvisor(5, 9);
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Sell"));
    }

    [Test]
    public void Update_ShouldReturnBuyRegression()
    {
        uut = new RegressionAdvisor(0.1f, 3);
        share = new Share("TestShare", uut);

        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 100 });
        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 110 });
        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 121 });

        string result = uut.Update(share);

        Assert.That(result, Is.EqualTo("Buy"));
    }

    [Test]
    public void Update_ShouldReturnSellRegression()
    {
        uut = new RegressionAdvisor(0.1f, 3);
        share = new Share("TestShare", uut);

        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 100 });
        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 90 });
        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 80 });

        string result = uut.Update(share);

        Assert.That(result, Is.EqualTo("Sell"));
    }

    [Test]
    public void Update_ShouldReturnKeepRegression()
    {
        uut = new RegressionAdvisor(0.1f, 3);

        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 100 });
        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 99 });
        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 101 });
        share.OnStockUpdate(this, new StockUpdateEventArgs { Time = DateTime.Now, Value = 101 });

        string result = uut.Update(share);

        Assert.That(result, Is.EqualTo("Keep"));
    }

    [Test]
    public void Update_ShouldHandleEdgeCases()
    {
        uut = new RegressionAdvisor(0.1f, 0);
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Keep"));

        uut = new RegressionAdvisor(0.1f, -1);
        result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Keep"));
    }
}