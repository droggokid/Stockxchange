using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Interfaces
{
    public interface IShare
    {
        DateTime GetValues(int numValues);
        void OnStockUpdate(object sender, StockUpdateEventArgs e);
        void StartSupervision(ITradeAdvisor tradeAdvisor, string strategy, float[] parameters);
    }
}