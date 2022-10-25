using System;

namespace Holding.Base.Event.General.SmsEvents.Dto
{
    public interface ISendMultiSmsItem 
    {

        Guid IndividualPersonId { get; }

        string CellPhoneNumber { get; }

    

    }
}