using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Interfaces
{
    public interface IShare
    {
        string Name {get;}
        List<(DateTime, float)> GetValues(int numValues);
        void OnStockUpdate(object? sender, StockUpdateEventArgs e);
        void StartSupervision(ITradeAdvisor tradeAdvisor, string strategy, float[] parameters);
        void TriggerRecommendedEvent(IShare share, string recommendation);
    }
}