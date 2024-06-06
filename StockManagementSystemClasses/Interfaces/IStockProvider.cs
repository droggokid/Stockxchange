namespace StockManagementSystemClasses.Interfaces
{
    public interface IStockProvider
    {
        bool ValidateShare(string share);
        void SubscribeStockProviderEvent(string share, EventHandler handler);
        void TriggerStockUpdateEvent(string shareName, DateTime time, float value);
    }
}