using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Models
{
    public class Share : IShare
    {
        public string Name {get;}
        public event EventHandler<StockRecommendedEventArgs> StockRecommendedEvent;
        private ITradeAdvisor tradeAdvisor;
        private float[] value;

        public Share(string name, ITradeAdvisor tradeAdvisor)
        {
            Name = name;
            this.tradeAdvisor = tradeAdvisor;
        }

        public void OnStockUpdate(object sender, StockUpdateEventArgs e)
        {
            
        }
        
        public (DateTime, float[]) GetValues(int numValues)
        {
            return (new DateTime(), new float[0]);
        }

        public void StartSupervision(ITradeAdvisor tradeAdvisor, string strategy, float[] parameters)
        {

        }

        public void TriggerRecommendedEvent(IShare share, string recommendation)
        {
            StockRecommendedEvent?.Invoke(this, new StockRecommendedEventArgs { Share = share, Recommendation = recommendation});
        }

        private void AppendValue(DateTime time, float value)
        {

        }

    }
}