using Holding.Base.DataProvider.FTS;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Text.RegularExpressions;
using Holding.Base.QueryService.SeedWorks.FTS;

namespace Holding.Base.DataProvider.Interceptors
{
    public class FtsInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuting( DbCommand command , DbCommandInterceptionContext<int> interceptionContext )
        {
        }

        public void NonQueryExecuted( DbCommand command , DbCommandInterceptionContext<int> interceptionContext )
        {
        }

        public void ReaderExecuting( DbCommand command , DbCommandInterceptionContext<DbDataReader> interceptionContext )
        {
            RewriteFullTextQuery( command );
        }

        public void ReaderExecuted( DbCommand command , DbCommandInterceptionContext<DbDataReader> interceptionContext )
        {
        }

        public void ScalarExecuting( DbCommand command , DbCommandInterceptionContext<object> interceptionContext )
        {
            RewriteFullTextQuery( command );
        }

        public void ScalarExecuted( DbCommand command , DbCommandInterceptionContext<object> interceptionContext )
        {
        }
       
        public static void RewriteFullTextQuery( DbCommand cmd )
        {
            var text = cmd.CommandText;
            for ( var i = 0 ; i < cmd.Parameters.Count ; i++ )
            {
                var parameter = cmd.Parameters[ i ];

                if ( !parameter.DbType.In( DbType.String , DbType.AnsiString , DbType.StringFixedLength , DbType.AnsiStringFixedLength ) ) continue;
                if ( parameter.Value == DBNull.Value ) continue;
                var value = ( string )parameter.Value;
                if ( value.IndexOf( FullTextPrefixes.ContainsPrefix , StringComparison.Ordinal ) >= 0 )
                {
                    parameter.Size = 4096;
                    parameter.DbType = DbType.AnsiStringFixedLength;
                    value = value.Replace( FullTextPrefixes.ContainsPrefix , "" );
                    value = value.Substring(1, value.Length - 2);
                    parameter.Value = value;
                    cmd.CommandText = Regex.Replace( text ,
                        string.Format(
                            @"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{0}\s?(?:ESCAPE N?'~')" , parameter.ParameterName ) ,
                        string.Format( @"CONTAINS([$1].[$2], @{0})" , parameter.ParameterName ) );
                    if ( text == cmd.CommandText )
                        throw new Exception( "FTS was not replaced on: " + text );
                    text = cmd.CommandText;
                }
                else if ( value.IndexOf( FullTextPrefixes.FreetextPrefix , StringComparison.Ordinal ) >= 0 )
                {
                    parameter.Size = 4096;
                    parameter.DbType = DbType.AnsiStringFixedLength;
                    value = value.Replace( FullTextPrefixes.FreetextPrefix , "" );
                    value = value.Substring( 1 , value.Length - 2 ); 
                    parameter.Value = value;
                    cmd.CommandText = Regex.Replace( text ,
                        string.Format(
                            @"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{0}\s?(?:ESCAPE N?'~')" , parameter.ParameterName ) ,
                        string.Format( @"FREETEXT([$1].[$2], @{0})" , parameter.ParameterName ) );
                    if ( text == cmd.CommandText )
                        throw new Exception( "FTS was not replaced on: " + text );
                    text = cmd.CommandText;
                }
            }
        }

    }
}
