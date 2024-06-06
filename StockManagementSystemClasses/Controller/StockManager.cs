using System;
using System.Collections.Generic;
using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;

namespace StockManagementSystemClasses.Controller
{
    public class StockManager : IStockManager
    {
        public event EventHandler<DisplayEventArgs> DisplayEvent;

        private IStockProvider stockProvider;
        private List<IShare> shares = new List<IShare>();

        public StockManager(IStockProvider provider)
        {
            stockProvider = provider;
        }

        public void OnSuperviseStock(object sender, SuperviseStockEventArgs e)
        {
            // Implement logic for supervising stocks
        }

        public void OnAddShare(object sender, AddShareEventArgs e)
        {
            // Implement logic for adding shares
        }

        public void OnStockRecommended(object sender, StockRecommendedEventArgs e)
        {

        }

        public void TriggerDisplayEvent(string msg)
        {
            DisplayEvent?.Invoke(this, new DisplayEventArgs { Message = msg });
        }

        public void AddShareToList(string name, IShare share)
        {
            shares.Add(share);
        }
    }
}
