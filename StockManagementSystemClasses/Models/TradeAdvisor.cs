using StockManagementSystemClasses.Interfaces;

namespace StockManagementSystemClasses.Models
{
    public class TradeAdvisor : ITradeAdvisor
    {
        public TradeAdvisor()
        {
            
        }
        
        public void setAdvisorStrategy(string strategy, float[] parameters)
        {
            
        }

        public string Update(IShare share)
        {
            return "";
        }
    }
}