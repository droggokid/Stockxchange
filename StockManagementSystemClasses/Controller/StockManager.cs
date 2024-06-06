using System;
using System.Collections.Generic;
using StockManagementSystemClasses.Interfaces;
using StockManagementSystemClasses.Events;
using StockManagementSystemClasses.Models;

namespace StockManagementSystemClasses.Controller
{
    public class StockManager : IStockManager
    {
        public event EventHandler<DisplayEventArgs>? DisplayEvent;

        private IStockProvider stockProvider;
        private ITradeAdvisor tradeAdvisor;
        private List<IShare> shares = new List<IShare>();

        public StockManager(IStockProvider provider, ITradeAdvisor advisor)
        {
            stockProvider = provider;
            tradeAdvisor = advisor;
        }

        public void OnSuperviseStock(object? sender, SuperviseStockEventArgs e)
        {
            IShare share = shares.Find(s => s.Name == e.ShareName);
            if(share != null)
            {
                share.StartSupervision(tradeAdvisor, e.Strategy, e.Parameters);
                TriggerDisplayEvent("Supervision started successfully");
            }
            else
            {
                TriggerDisplayEvent("Share not found");
            }
        }

        public void OnAddShare(object? sender, AddShareEventArgs e)
        {
            if(e.Share != null && stockProvider.ValidateShare(e.Share))
            {
                IShare share = new Share(e.Share, tradeAdvisor);
                stockProvider.SubscribeStockProviderEvent(e.Share, share.OnStockUpdate);
                AddShareToList(e.Share, share);
                TriggerDisplayEvent("Share added successfully");
            }
            else
            {
                TriggerDisplayEvent("Share not valid");
            }
        }

        public void OnStockRecommended(object sender, StockRecommendedEventArgs e)
        {
            if(e.Recommendation != null)
            {
                TriggerDisplayEvent(e.Recommendation);
            }
        }

        public void TriggerDisplayEvent(string msg)
        {
            if (msg != null)
            {
                DisplayEvent?.Invoke(this, new DisplayEventArgs { Message = msg });
            }
        }

        public void AddShareToList(string name, IShare share)
        {
            shares.Add(share);
        }
    }
}
