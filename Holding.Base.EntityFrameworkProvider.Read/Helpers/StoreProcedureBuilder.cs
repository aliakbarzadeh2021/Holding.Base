using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Holding.Base.EntityFrameworkProvider.Read.Attributes;
using Holding.Base.Infrastructure.Configuration;

namespace Holding.Base.EntityFrameworkProvider.Read.Helpers
{
    public class StoreProcedureBuilder<T>
    {
        private string _spName;
        private string _schema = "dbo";
        private string _body;
        private string _whereCluse;

        private string _existAndAlter;
        public StoreProcedureBuilder()
        {
            CreateExistCommand(_schema);
        }

        public StoreProcedureBuilder(string schema)
        {
            CreateExistCommand(schema);
        }

        private void CreateExistCommand(string schemaName)
        {
            if (_schema != null && _schema != schemaName) _schema = schemaName;
            var attr = typeof(T).CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(StoredProcedureAttribute));
            if (attr == null)
                throw new Exception("CreateExistCommand error.");
            _spName = attr.ConstructorArguments[0].Value.ToString();
            _existAndAlter = string.Format("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES " +
                                           "WHERE ROUTINE_NAME = '{0}' AND ROUTINE_SCHEMA = '{1}' " +
                                           "AND ROUTINE_TYPE = 'PROCEDURE')", _spName, schemaName);
        }

        public StoreProcedureBuilder<T> Schema(string schema)
        {
            _schema = schema;
            return this;
        }

        public StoreProcedureBuilder<T> Body(string body)
        {
            _body = body;
            return this;
        }

        public StoreProcedureBuilder<T> WhereCluse(string whereCluse)
        {
            _whereCluse = whereCluse;
            return this;
        }

        /// <summary>
        /// Builder method.
        /// </summary>
        public void Execute()
        {
            var parameters = string.Empty;
            foreach (var fieldInfo in typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(
                f => f.CustomAttributes.Any(x => x.AttributeType == typeof(SpParameterAttribute))))
            {
                var parameterName = fieldInfo.CustomAttributes.FirstOrDefault().NamedArguments[0].TypedValue.Value.ToString();
                var parameterType = fieldInfo.CustomAttributes.FirstOrDefault().NamedArguments[1].TypedValue.Value.ToString();
                parameters += string.Format("{0}@{1} {2}", (parameters == string.Empty ? string.Empty : ","), parameterName, parameterType);
            }
            var procedure = string.Format("PROCEDURE {0}.{1} \n{2}\nAS BEGIN\n{3}\n{4}\nEND;", _schema, _spName, parameters, _body, _whereCluse);
            var executeQuery = string.Format("{0} BEGIN EXEC('ALTER {1}') END ELSE BEGIN EXEC('CREATE {1}') END;", _existAndAlter, procedure);
            using (var connection = new SqlConnection(ApplicationSettingsFactory.GetApplicationSettings().SqlConnectionString))
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                new SqlCommand(executeQuery, connection).ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}