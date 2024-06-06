using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Models
{
    
    public class UI : IUI
    {
        public event EventHandler<AddShareEventArgs>? AddShareEvent;
        public event EventHandler<SuperviseStockEventArgs>? SuperviseStockEvent;
        
        public void OnDisplay(object sender, string msg)
        {
            Console.WriteLine(msg);
        }

        public void TriggerAddShareEvent(string share)
        {
            AddShareEvent?.Invoke(this, new AddShareEventArgs { Share = share });
        }

        public void TriggerSuperviseStockEvent(string shareName, string strategy, float[] parameters)
        {
            SuperviseStockEvent?.Invoke(this, new SuperviseStockEventArgs { ShareName = shareName, Strategy = strategy, Parameters = parameters });
        }
    }
}