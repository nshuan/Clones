namespace Core.InfiniteListView
{
    public interface ISetupElement<T> where T : IElementInfo
    {
        int ElementId { get; set; }
        T ElementInfo { get; set; }
    }
}