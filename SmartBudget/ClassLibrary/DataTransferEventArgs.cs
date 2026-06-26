namespace SmartBudget.ClassLibrary
{
    public class DataTransferEventArgs(List<ObjectOfAnalysis> data) : EventArgs
    {
        public List<ObjectOfAnalysis> OperationsData { get; set; } = data;
    }
}