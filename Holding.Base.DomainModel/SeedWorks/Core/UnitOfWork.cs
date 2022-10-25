using System;
using System.Collections.Generic;
using System.IO;

namespace Holding.Base.DomainModel.SeedWorks.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly List<Action> _uncommitedActions;
        private readonly Dictionary<string , object> _eventBag;
        public UnitOfWork()
        {
            _uncommitedActions = new List<Action>();
            _eventBag = new Dictionary<string , object>();
        }
        public void Attach( Action operation )
        {
            _uncommitedActions.Add( operation );
        }

        public void RegisterEventInToBag<TEvent>( TEvent @event )
        {
            var eventType = typeof( TEvent ).FullName;
            if ( _eventBag.ContainsKey( eventType ) )
                _eventBag.Remove( eventType );

            _eventBag.Add( eventType , @event );
        }

        public TEvent LoadEventFromBag<TEvent>()
        {
            var eventType = typeof( TEvent ).FullName;
            if ( !_eventBag.ContainsKey( eventType ) )
                throw new InvalidDataException( string.Format( "Event with type {0} is not registered...." , eventType ) );
            return ( TEvent )_eventBag[ eventType ];
        }

        public void Commit()
        {
            _uncommitedActions.ForEach( operation => operation.Invoke() );
            _uncommitedActions.Clear();
            _eventBag.Clear();
        }
    }
}
