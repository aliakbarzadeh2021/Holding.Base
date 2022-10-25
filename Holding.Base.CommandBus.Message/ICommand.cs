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

using System;

namespace Holding.Base.CommandBus.Message
{
    public interface ICommand : IMessage
    {
        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        Guid CommandId { get; }

        /// <summary>
        /// دریافت متن لاگ
        /// </summary>
        /// <returns>متن لاگ فرمان را بر می گرداند
        /// از این متن در لاگ استفاده می شود
        /// </returns>
        string GetLogMessage();

        string Name { get; }
    }
}
