using System.Collections.Generic;
using Holding.Base.Event.Message.Core;
using Holding.Base.Event.User;

namespace Holding.Base.Event.Message.User
{
    public class UserCreatedEvent : EventBase , IUserCreatedEvent
    {
        public UserCreatedEvent( string id , string name , string password , string passwordSalt , string passwordQuestion , string passwordAnswer , IList<IUserRoleEvent> roles )
        {
            Id = id;
            UserName = name;
            Password = password;
            PasswordSalt = passwordSalt;
            PasswordQuestion = passwordQuestion;
            PasswordAnswer = passwordAnswer;
            Roles = roles;
        }

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string PasswordSalt { get; private set; }
        public string PasswordQuestion { get; private set; }
        public string PasswordAnswer { get; private set; }
        public IList<IUserRoleEvent> Roles { get; private set; }
    }
}
