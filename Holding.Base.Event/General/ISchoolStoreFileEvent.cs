using System;

namespace Holding.Base.Event.General
{
    public interface ISchoolStoreFileEvent
    {
        string FileName { get; }
        Guid SchoolId { get; }
    }
}