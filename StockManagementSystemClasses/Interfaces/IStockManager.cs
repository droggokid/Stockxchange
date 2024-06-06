using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Interfaces
{
    public interface IStockManager
    {
        event EventHandler<DisplayEventArgs> DisplayEvent;

        void OnSuperviseStock(object? sender, SuperviseStockEventArgs e);
        void OnAddShare(object? sender, AddShareEventArgs e);
        void OnStockRecommended(object sender, StockRecommendedEventArgs e);
        void TriggerDisplayEvent(string msg);
        void AddShareToList(string name, IShare share);
    }
}