using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Interfaces
{
    public interface IUI
    {
        event EventHandler<AddShareEventArgs> AddShareEvent;
        event EventHandler<SuperviseStockEventArgs> SuperviseStockEvent;
        void OnDisplay(object sender, string msg);
        void TriggerAddShareEvent(string share);
        void TriggerSuperviseStockEvent(string shareName, string strategy, float[] parameters);
    }
}