namespace StockManagementSystemClasses.Events
{
    using System;
    using StockManagementSystemClasses.Models;

    public class StockRecommendedEventArgs : EventArgs
    {
        public Share? Share { get; set; }
        public string? Recommendation { get; set; }
    }

    public class StockUpdateEventArgs : EventArgs
    {
        public string? ShareName { get; set; }
        public DateTime Time { get; set; }
        public float Value { get; set; }
    }
}
