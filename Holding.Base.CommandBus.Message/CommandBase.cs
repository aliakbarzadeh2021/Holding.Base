using System;

namespace Holding.Base.CommandBus.Message
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CommandBase : RequestBase, ICommand
    {
        protected CommandBase()
        {
            CommandId = Guid.NewGuid();
        }


        /// <summary>
        /// Command name.
        /// </summary>
        public virtual string Name { get { return GetType().Name; } }

        public Guid CommandId { get; private set; }


        public virtual string GetLogMessage()
        {
            return string.Empty;
        }

        public abstract void Validate();
    }

}
