namespace StockManagementSystemClasses.Events
{
    using System;
    using StockManagementSystemClasses.Interfaces;

    public class StockRecommendedEventArgs : EventArgs
    {
        public IShare? Share { get; set; }
        public string? Recommendation { get; set; }
    }

    public class StockUpdateEventArgs : EventArgs
    {
        public string? ShareName { get; set; }
        public DateTime Time { get; set; }
        public float Value { get; set; }
    }

    public class AddShareEventArgs : EventArgs
    {
        public string? Share { get; set; }
    }

    public class SuperviseStockEventArgs : EventArgs
    {
        public string? ShareName { get; set; }
        public string? Strategy { get; set; }
        public float[]? Parameters { get; set; }
    }

    public class DisplayEventArgs : EventArgs
    {
        public string? Message { get; set; }
    }

    public enum RecommendationStrategy
    {
        NoAdvisor,
        LimitAdvisor,
        RegressionAdvisor
    }
}
