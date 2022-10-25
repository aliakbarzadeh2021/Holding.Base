using System.Threading.Tasks;
using Holding.Base.SOA.Formatters;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using WebApiDoodle.Net.Http.Client;
using WebApiDoodle.Net.Http.Client.Model;

namespace Holding.Base.SOA.SeedWorks.Core
{
    public abstract class ApiClient<TDto> : HttpApiClient<TDto> where TDto : IDto
    {
        protected ApiClient( HttpClient client )
            : base( client , MediaTypeFormatters.Instance )
        {
        }

        protected ApiClient( HttpClient client , IEnumerable<MediaTypeFormatter> formatters )
            : base( client , formatters )
        {
        }

        protected async Task<TResult> HandleResponseAsync<TResult>( Task<HttpApiResponseMessage<TResult>> responseTask )
        {
            using ( var apiResponse = await responseTask )
            {
                if ( apiResponse.IsSuccess )
                {
                    return apiResponse.Model;
                }

                throw GetHttpApiRequestException( apiResponse );
            }
        }

        protected async Task HandleResponseAsync( Task<HttpApiResponseMessage> responseTask )
        {
            using ( var apiResponse = await responseTask )
            {
                if ( !apiResponse.IsSuccess )
                {
                    throw GetHttpApiRequestException( apiResponse );
                }
            }
        }

        protected HttpApiRequestException GetHttpApiRequestException( HttpApiResponseMessage apiResponse )
        {
            return new HttpApiRequestException(
                string.Format( "Http request format is invalid..." ) ,
                    apiResponse.Response.StatusCode ,
                    apiResponse.HttpError );
        }
    }
}
