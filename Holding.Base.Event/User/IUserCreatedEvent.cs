using System.Collections.Generic;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.User
{
    public interface IUserCreatedEvent : IMessage
    {
        string Id { get; }
        string UserName { get; }
        string Password { get; }
        string PasswordSalt { get; }
        string PasswordQuestion { get; }
        string PasswordAnswer { get; }
        IList<IUserRoleEvent> Roles { get; }
    }
}
