using StockManagementSystemClasses.Interfaces;

namespace StockManagementSystemClasses.Models
{

    public class RegressionAdvisor : ITradeAdvisor
    {
        private float _percentageChange;
        private int _samples;
        private Queue<float> _valueHistory = new Queue<float>();

        public RegressionAdvisor()
        {
        }

        public string Update(Share share)
        {
            float currentValue = share.GetValues(1)[0].Item2;
            _valueHistory.Enqueue(currentValue);
            if (_valueHistory.Count > _samples)
            // nødvendig hvis queue ikke var tom til at starte med o_O
                _valueHistory.Dequeue();

            if (_valueHistory.Count == _samples)
            {
                if (_valueHistory.Count > 0) 
                {
                    float initial = _valueHistory.Peek();
                    float change = (currentValue - initial) / initial * 100;

                    if (change > _percentageChange)
                    {
                        return "Buy";
                    }
                    else if (change < -_percentageChange)
                    {
                        return "Sell";
                    }
                    else
                    {
                        return "Keep";
                    }
                }
            }
            return "Keep";
        }
    }
}


