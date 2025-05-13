using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Models
{
    public class Share
    {
        public string Name {get;}
        public event EventHandler<StockRecommendedEventArgs>? StockRecommendedEvent;
        private ITradeAdvisor _tradeAdvisor { get; set;}
        private List<(DateTime, float)> values = new List<(DateTime, float)>();

        public Share(string name, ITradeAdvisor tradeAdvisor)
        {
            Name = name;
            _tradeAdvisor = tradeAdvisor;
            values = new List<(DateTime, float)>(); 
        }

        public void OnStockUpdate(object? sender, StockUpdateEventArgs e)
        {
            AppendValue(e.Time, e.Value);
            string recommendation = _tradeAdvisor.Update(this);
            TriggerRecommendedEvent(this, recommendation);
        }
        
        public List<(DateTime, float)> GetValues(int numValues)
        {
            if(numValues <= 0)
            {
                return new List<(DateTime, float)> { values[0] };
            }
            if(numValues > values.Count)
            {
                return new List<(DateTime, float)> { values[values.Count - 1] };
            }
            return values.GetRange(values.Count - numValues, numValues);
        }

        public void StartSupervision(ITradeAdvisor tradeAdvisor)
        {
            _tradeAdvisor = tradeAdvisor;
        }

        public void TriggerRecommendedEvent(Share share, string recommendation)
        {
            StockRecommendedEvent?.Invoke(this, new StockRecommendedEventArgs { Share = share, Recommendation = recommendation});
        }

        private void AppendValue(DateTime time, float value)
        {
            values.Add((time, value));
        }
    }
}