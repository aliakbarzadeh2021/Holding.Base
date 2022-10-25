
namespace Holding.Base.Bus.Configuration
{
    public interface IMessageQueue
    {
        string Name { get; }
        string Address { get; }        
    }
}
