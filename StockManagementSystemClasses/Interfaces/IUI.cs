using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Interfaces
{
    public interface IUI
    {
        void OnDisplay(object sender, string msg);
        void TriggerAddShareEvent(string share);
        void TriggerSuperviseStockEvent(string shareName, string strategy, float[] parameters);
    }
}