using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Models
{
    
    public class StockProvider : IStockProvider
    {
        public static StockProvider Instance { get; } = new StockProvider();
        public event EventHandler<StockUpdateEventArgs> StockUpdateEvent;
        
        public bool ValidateShare(string share)
        {
            return true;
        }

        public void SubscribeStockProviderEvent(string share, EventHandler handler)
        {
            
        }

        public void TriggerStockUpdateEvent(string shareName, DateTime time, float value)
        {
            StockUpdateEvent?.Invoke(this, new StockUpdateEventArgs { ShareName = shareName, Time = time, Value = value });
        }
    }
}
