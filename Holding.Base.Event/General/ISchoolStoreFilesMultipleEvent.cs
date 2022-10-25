using System;

namespace Holding.Base.Event.General
{
    public interface ISchoolStoreFilesMultipleEvent
    {
        string[] FileNames { get; }
        Guid SchoolId { get; }
    }
}