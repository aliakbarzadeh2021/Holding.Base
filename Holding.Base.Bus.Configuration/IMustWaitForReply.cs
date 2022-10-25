
using System;
using MassTransit;

namespace Holding.Base.Bus.Configuration
{
    public interface IMustWaitForReply
    {
        void HandleResponse(Action action, IConsumeContext respondContext);
    }
}
