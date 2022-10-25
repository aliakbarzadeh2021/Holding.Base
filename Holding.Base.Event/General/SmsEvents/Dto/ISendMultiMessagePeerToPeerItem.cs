using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holding.Base.Event.General.SmsEvents.Dto
{
    public interface  ISendMultiMessagePeerToPeerItem
    {
         string CellPhoneNumber { get;  }
         string Text { get; }
         Guid IndividualPersonId { get; }
    }
}
