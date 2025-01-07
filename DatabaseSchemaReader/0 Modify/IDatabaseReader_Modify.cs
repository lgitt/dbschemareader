using System.Collections.Generic;

using DatabaseSchemaReader.DataSchema;
using DatabaseSchemaReader.Filters;

namespace DatabaseSchemaReader
{
    public partial interface IDatabaseReader
    {
        /// <summary> Gets all views (just names, no columns). </summary>
        IList<DatabaseView> ViewList();

        /// <summary> Exclude specified items when reading schema </summary>
        /// <value> The exclusions. </value>
        Exclusions Exclusions { get; }
    }
}