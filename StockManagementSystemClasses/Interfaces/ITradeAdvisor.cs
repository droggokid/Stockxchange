namespace StockManagementSystemClasses.Interfaces
{
    public interface ITradeAdvisor
    {
        void setAdvisorStrategy(string strategy, float[] parameters);
        string Update(IShare share);
    }
}