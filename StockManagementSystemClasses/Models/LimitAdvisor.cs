using StockManagementSystemClasses.Interfaces;


namespace StockManagementSystemClasses.Models
{

    public class LimitAdvisor : ITradeAdvisor
    {
        private float _thBuy;
        private float _thSell;

        public LimitAdvisor(float thBuy, float thSell)
        {
            _thBuy = thBuy;
            _thSell = thSell;
        }

        public string Update(Share share)
        {
            float currentValue = share.GetValues(1)[0].Item2;

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
                
        }
    }
}


