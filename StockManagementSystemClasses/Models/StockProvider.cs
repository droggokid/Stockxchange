using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Models
{
    
    public class StockProvider : IStockProvider
    {
        public static StockProvider Instance { get; } = new StockProvider();
        public event EventHandler<StockUpdateEventArgs>? StockUpdateEvent;
        
        public bool ValidateShare(string share)
        {
            return !string.IsNullOrEmpty(share);
        }

        public void SubscribeStockProviderEvent(string share, EventHandler<StockUpdateEventArgs> handler)
        {
            var stockUpdateEventArgs = new StockUpdateEventArgs { ShareName = share };
            StockUpdateEvent += (sender, args) =>
            {
                stockUpdateEventArgs.Time = args.Time;
                stockUpdateEventArgs.Value = args.Value;
                handler(sender, stockUpdateEventArgs);
            };
        }
    }
}
