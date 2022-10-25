namespace Holding.Base.Event.General
{
    public interface IStoreFilesMultipleEvent
    {
        string[] FileNames { get; }
    }
}