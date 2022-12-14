// ==============================================================================================================
// Microsoft patterns & practices
// CQRS Journey project
// ==============================================================================================================
// ©2012 Microsoft. All rights reserved. Certain content used with permission from contributors
// http://go.microsoft.com/fwlink/p/?LinkID=258575
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.
// ==============================================================================================================

/*namespace Holding.Base.Bus.SimpleServiceBus.Handling
{

    /// <summary>
    /// API-oriented class.
    /// </summary>
    /// <typeparam name="TMessage">The message type to consume.</typeparam>
    public static class Consumes<TMessage>
        where TMessage : class
    {
        static readonly Selected _null;

        static Consumes()
        {
            _null = new NullConsumer();
        }

        public static Selected Null
        {
            get { return _null; }
        }

        class NullConsumer :
            Selected
        {
            //static readonly ILog _log = Logger.Get(typeof(NullConsumer));
            readonly string _message;

            public NullConsumer()
            {
                _message = "A message of type " + typeof(TMessage).Name + " was discarded: (NullConsumer)";
            }

            public bool Accept(TMessage message)
            {
                return true;
            }

            public void Consume(TMessage message)
            {
                //_log.Warn(_message);
            }
        }

        /// <summary>
        /// Declares a Consume method for the message type TMessage which is called
        /// whenever a a message is received of the specified type.
        /// </summary>
        public interface All :
            IConsumer
        {
            /// <summary>
            /// Called by the framework when a message is available to be consumed. This
            /// is called by a framework thread, so care should be used when accessing
            /// any shared objects.
            /// </summary>
            /// <param name="message">The message to consume.</param>
            void Consume(TMessage message);
        }


        /// <summary>
        /// Declares a selective consumer method for the message type TMessage. In addition
        /// to the Consume(TMessage) method, an additional Accept method is used to allow
        /// the consumer object to accept or ignore the message before it is delivered to
        /// the consumer.
        /// </summary>
        public interface Selected :
            All
        {
            /// <summary>
            /// Called by the framework when a message is available. If the consumer is
            /// interested in the message, returning true will result in the Consume method
            /// being called once the message has been read from the transport.
            /// 
            /// It is important that no actual processing of the message is done during the
            /// accept method, as there is no guarantee that the Consume method will be called
            /// for an accepted message. Therefore, any allocation of resources or locks should
            /// be acquired only once the message is delivered via the Consume method.
            /// </summary>
            /// <param name="message">The message to accept or ignore</param>
            /// <returns>True if the consumer wants to Consume the message, otherwise false to ignore it.</returns>
            bool Accept(TMessage message);
        }
    }

    public interface IConsumer
    {
    }
}*/
