using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DatabaseSchemaReader.DataSchema;
using DatabaseSchemaReader.ProviderSchemaReaders.ConnectionContext;

namespace DatabaseSchemaReader.ProviderSchemaReaders.Databases.PostgreSql  
{
    class IdentityColumns : SqlExecuter<DatabaseColumn>
    {
        private readonly string _tableName;

        public IdentityColumns(int? commandTimeout, string owner, string tableName)
            : base(commandTimeout, owner)
        {
            _tableName = tableName;
			//increment is always 1
            Sql = @"SELECT
table_schema AS SchemaOwner,
TABLE_NAME AS TableName,
COLUMN_NAME AS ColumnName
FROM information_schema.columns
WHERE is_identity='YES' AND
(table_schema = :OWNER OR :OWNER IS NULL)
AND (table_name = :TABLENAME OR :TABLENAME IS NULL)";
        }

        public IList<DatabaseColumn> Execute(IConnectionAdapter connectionAdapter)
        {
            ExecuteDbReader(connectionAdapter);
            return Result;
        }

        protected override void AddParameters(DbCommand command)
        {
           
            AddDbParameter(command, "OWNER", Owner);
            AddDbParameter(command, "TABLENAME", _tableName);
        }

        protected override void Mapper(IDataRecord record)
        {
            var schema = record.GetString("schemaowner");
            var tableName = record.GetString("tablename");
            var columnName = record.GetString("columnname");
            var column = new DatabaseColumn
            {
                SchemaOwner = schema,
                TableName = tableName,
                Name = columnName,
				IsAutoNumber = true,
                IdentityDefinition = new DatabaseColumnIdentity(),
            };

            Result.Add(column);
        }
    }
}
