using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Interfaces
{
    public interface ITradeAdvisor
    {
        public RecommendationStrategy _strategy { get; set; }
        void setAdvisorStrategy(string strategy, float[] parameters);
        string Update(IShare share);
    }
}