using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Interfaces
{
    public interface IStockProvider
    {
        bool ValidateShare(string share);
        void SubscribeStockProviderEvent(string share, EventHandler<StockUpdateEventArgs> handler);
    }
}