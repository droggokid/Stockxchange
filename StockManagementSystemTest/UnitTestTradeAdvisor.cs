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
    }

    [Test]
    public void SetAdvisorStrategy_ShouldSetStrategy1()
    {
        uut.setAdvisorStrategy("NoAdvisor", new float[] { 1, 2, 3 });
        Assert.That(uut._strategy, Is.EqualTo(RecommendationStrategy.NoAdvisor));
    }

    [Test]
    public void SetAdvisorStrategy_ShouldSetStrategy2()
    {
        uut.setAdvisorStrategy("LimitAdvisor", new float[] { 1, 2, 3 });
        Assert.That(uut._strategy, Is.EqualTo(RecommendationStrategy.LimitAdvisor));
    }
    
    [Test]
    public void SetAdvisorStrategy_ShouldSetStrategy3()
    {
        uut.setAdvisorStrategy("RegressionAdvisor", new float[] { 1, 2, 3 });
        Assert.That(uut._strategy, Is.EqualTo(RecommendationStrategy.RegressionAdvisor));
    }
}