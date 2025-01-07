using System.Collections.Generic;

using DatabaseSchemaReader.DataSchema;

namespace DatabaseSchemaReader
{
    public partial class DatabaseReader
    {
        /// <summary> Gets all views (just names, no columns). </summary>
        public IList<DatabaseView> ViewList()
        {
            RaiseReadingProgress(SchemaObjectType.Tables);
            using (_readerAdapter.CreateConnection())
            {
                return _readerAdapter.Views(null);
            }
        }
    }
}