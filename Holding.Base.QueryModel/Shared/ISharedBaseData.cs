using System.Collections.Generic;

namespace Holding.Base.QueryModel.Shared
{
    public interface ISharedBaseData
    {
        IEnumerable<SchoolType> SchoolTypes { get; }

        IEnumerable<Country> Countries { get; }

        IEnumerable<Province> Provinces { get; }

        IEnumerable<City> Cities { get; }

        IEnumerable<SchoolSegment> SchoolSegments { get; }

        IEnumerable<Religion> Religions { get; }     
    }
}
