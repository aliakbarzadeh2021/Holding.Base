using Holding.Base.Cache.Core.DependencyInjection;
using Holding.Base.Infrastructure.Querying;
using Holding.Base.QueryModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Holding.Base.SharedData.Cache
{
    class SharedBaseData : ISharedBaseData
    {
        private readonly IQueryDataProvider _queryProvider;

        public SharedBaseData( IQueryDataProvider queryProvider )
        {
            _queryProvider = queryProvider;
        }

        public IEnumerable<SchoolType> SchoolTypes
        {
            get
            {
                return AppServices.Cache.Get( "SchoolTypes" , TimeSpan.FromDays( 365 ) , () => _queryProvider.Query<SchoolType>().ToList());
            }
        }

        public IEnumerable<Country> Countries
        {
            get
            {
                return AppServices.Cache.Get( "Countries" , TimeSpan.FromDays( 365 ) , () => _queryProvider.Query<Country>().ToList() );
            }
        }
        public IEnumerable<Province> Provinces
        {
            get
            {
                return AppServices.Cache.Get( "Provinces" , TimeSpan.FromDays( 365 ) , () => _queryProvider.Query<Province>().ToList() );
            }
        }
        public IEnumerable<City> Cities
        {
            get
            {
                return AppServices.Cache.Get( "Cities" , TimeSpan.FromDays( 365 ) , () => _queryProvider.Query<City>().ToList() );
            }
        }

        public IEnumerable<SchoolSegment> SchoolSegments
        {
            get
            {
                return AppServices.Cache.Get( "SchoolSegments" , TimeSpan.FromDays( 365 ) , () =>_queryProvider.Query<SchoolSegment>().ToList() );
            }
        }

        public IEnumerable<Religion> Religions
        {
            get
            {
                return AppServices.Cache.Get( "Religions" , TimeSpan.FromDays( 365 ) , () => _queryProvider.Query<Religion>().ToList() );
            }
        }        
    }
}
