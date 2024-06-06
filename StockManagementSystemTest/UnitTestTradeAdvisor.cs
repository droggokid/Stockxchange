using StockManagementSystemClasses.Controller;
using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;
using StockManagementSystemClasses.Models;
using NUnit.Framework;
using NSubstitute;

namespace StockManagementSystemTest;

public class TradeAdvisorTests
{
    private ITradeAdvisor uut;
    private IShare share;
    private IStockManager stockManager;
    private IStockProvider stockProvider;

    [SetUp]
    public void Setup()
    {
        stockProvider = Substitute.For<IStockProvider>();
        uut = new TradeAdvisor();
        share = new Share("TestShare", uut);
        stockManager = new StockManager(stockProvider, uut);
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 10 };
        share.OnStockUpdate(this, args);
    }

    [Test]
    public void SetAdvisorStrategy_ShouldSetStrategy1()
    {
        uut.setAdvisorStrategy("NoAdvisor", new float[] { 1, 2 });
        Assert.That(uut._strategy, Is.EqualTo(RecommendationStrategy.NoAdvisor));
    }

    [Test]
    public void SetAdvisorStrategy_ShouldSetStrategy2()
    {
        uut.setAdvisorStrategy("LimitAdvisor", new float[] { 1, 2 });
        Assert.That(uut._strategy, Is.EqualTo(RecommendationStrategy.LimitAdvisor));
    }
    
    [Test]
    public void SetAdvisorStrategy_ShouldSetStrategy3()
    {
        uut.setAdvisorStrategy("RegressionAdvisor", new float[] { 1, 2 });
        Assert.That(uut._strategy, Is.EqualTo(RecommendationStrategy.RegressionAdvisor));
    }

    [Test]
    public void Update_ShouldReturnEmptyString()
    {
        uut.setAdvisorStrategy("NoAdvisor", new float[] { 1, 2 });
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void Update_ShouldReturnBuy()
    {
        uut.setAdvisorStrategy("LimitAdvisor", new float[] { 15, 20 });
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Buy"));
    }

    [Test]
    public void Update_ShouldReturnKeep()
    {

        uut.setAdvisorStrategy("LimitAdvisor", new float[] { 10, 20 });
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Keep"));
    }

    [Test]
    public void Update_ShouldReturnSell()
    {

        uut.setAdvisorStrategy("LimitAdvisor", new float[] { 5, 9 });
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Sell"));
    }

    [Test]
    public void Update_ShouldReturnBuyRegression()
    {
        uut.setAdvisorStrategy("RegressionAdvisor", new float[] { 0.1f, 3 });
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 100 };
        share.OnStockUpdate(this, args);
        
        StockUpdateEventArgs args1 = new StockUpdateEventArgs { Time = DateTime.Now, Value = 110 };
        share.OnStockUpdate(this, args1);
        
        StockUpdateEventArgs args2 = new StockUpdateEventArgs { Time = DateTime.Now, Value = 121 };
        share.OnStockUpdate(this, args2);
        
        string result = uut.Update(share);
        
        Assert.That(result, Is.EqualTo("Buy"));
    }

    [Test]
    public void Update_ShouldReturnSellRegression()
    {
        uut.setAdvisorStrategy("RegressionAdvisor", new float[] { 0.1f, 3 });
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 100 };
        share.OnStockUpdate(this, args);
        
        StockUpdateEventArgs args1 = new StockUpdateEventArgs { Time = DateTime.Now, Value = 90 };
        share.OnStockUpdate(this, args1);
        
        StockUpdateEventArgs args2 = new StockUpdateEventArgs { Time = DateTime.Now, Value = 80 };
        share.OnStockUpdate(this, args2);
        
        string result = uut.Update(share);
        
        Assert.That(result, Is.EqualTo("Sell"));
    }

    [Test]
    public void Update_ShouldReturnKeepRegression()
    {
        uut.setAdvisorStrategy("RegressionAdvisor", new float[] { 0.1f, 3 });
        StockUpdateEventArgs args = new StockUpdateEventArgs { Time = DateTime.Now, Value = 100 };
        share.OnStockUpdate(this, args);
        
        StockUpdateEventArgs args1 = new StockUpdateEventArgs { Time = DateTime.Now, Value = 99 };
        share.OnStockUpdate(this, args1);
        
        StockUpdateEventArgs args2 = new StockUpdateEventArgs { Time = DateTime.Now, Value = 101 };
        share.OnStockUpdate(this, args2);

        StockUpdateEventArgs args3 = new StockUpdateEventArgs { Time = DateTime.Now, Value = 101 };
        share.OnStockUpdate(this, args3);

        string result = uut.Update(share);

        Assert.That(result, Is.EqualTo("Keep"));
    }

    [Test]
    public void Update_ShouldHandleEdgeCases()
    {
        uut.setAdvisorStrategy("RegressionAdvisor", new float[] { 0.1f, 0 });
        string result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Keep"));

        uut.setAdvisorStrategy("RegressionAdvisor", new float[] { 0.1f, -1 });
        result = uut.Update(share);
        Assert.That(result, Is.EqualTo("Keep"));
    }

}