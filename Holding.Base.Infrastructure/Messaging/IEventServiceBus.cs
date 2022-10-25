namespace Holding.Base.Infrastructure.Messaging
{
    public interface IEventServiceBus
    {
        IBus Bus { get; }
    }
}