using StockManagementSystemClasses.Interfaces;

namespace StockManagementSystemClasses.Models
{
    public enum RecommendationStrategy
    {
        NoAdvisor,
        LimitAdvisor,
        RegressionAdvisor
    }

    public class TradeAdvisor : ITradeAdvisor
    {
        private RecommendationStrategy _strategy;
        private float _thBuy;
        private float _thSell;
        private float _percentageChange;
        private int _samples;
        private Queue<float> _valueHistory = new Queue<float>();

        public TradeAdvisor()
        {
            _strategy = RecommendationStrategy.NoAdvisor;
        }
        
        public void setAdvisorStrategy(string strategy, float[] parameters)
        {
            switch (strategy)
            {
                case "NoAdvisor":
                    _strategy = RecommendationStrategy.NoAdvisor;
                    break;
                case "LimitAdvisor":
                    if(parameters.Length >= 2)
                    {
                        _strategy = RecommendationStrategy.LimitAdvisor;
                        _thBuy = parameters[0];
                        _thSell = parameters[1];
                    }
                    break;
                case "RegressionAdvisor":
                    if(parameters.Length >= 2)
                    {
                        _strategy = RecommendationStrategy.RegressionAdvisor;
                        _percentageChange = parameters[0];
                        _samples = (int)parameters[1];
                    }
                    break;
                default:
                    _strategy = RecommendationStrategy.NoAdvisor;
                    break;
            }
        }

        public string Update(IShare share)
        {
            float currentValue = share.GetValues(1).Item2;

            switch (_strategy)
            {
                case RecommendationStrategy.NoAdvisor:
                    return "";
                case RecommendationStrategy.LimitAdvisor:
                    if(currentValue < _thBuy)
                    {
                        return "Buy";
                    }
                    else if(currentValue > _thSell)
                    {
                        return "Sell";
                    }
                    else
                    {
                        return "Keep";
                    }
                case RecommendationStrategy.RegressionAdvisor:
                    _valueHistory.Enqueue(currentValue);
                    // nødvendig hvis queue ikke var tom til at starte med o_O
                    if (_valueHistory.Count > _samples)
                        _valueHistory.Dequeue();
                    
                    if (_valueHistory.Count == _samples)
                    {
                        // (slutværdi-startværdi)/startværdi*100%=procentdel
                        float initial = _valueHistory.Peek();
                        float change = (currentValue - initial) / initial * 100;
                        if (currentValue > change/_samples && change/_samples > 0)
                        {
                            return "Buy";
                        }
                        else if (currentValue < change/_samples && change/_samples < 0)
                        {
                            return "Sell";
                        }
                        else
                        {
                            return "Keep";
                        }
                    }
                    return "Keep";
                default:
                    return "";
            }
        }
    }
}