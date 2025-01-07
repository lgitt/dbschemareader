using JCommon;

namespace DatabaseSchemaReader.DataSchema
{
    /// <summary>A column in the database</summary>
    public partial class DatabaseColumn
    {
        private string _columnNamePinYin;
        private string _columnNamePinYinFirstLetter;

        private string _displayName;

        private string _entityName;

        /// <summary>显示名称</summary>
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(_displayName))
                {
                    _displayName = Name;
                }
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }

        /// <summary>拼音</summary>
        public string ColumnNamePinYin
        {
            get
            {
                if (string.IsNullOrEmpty(_columnNamePinYin))
                {
                    _columnNamePinYin = JPinYin.Get(Name);
                }
                return _columnNamePinYin;
            }
            set
            {
                _columnNamePinYin = value;
            }
        }

        /// <summary>拼音首字母</summary>
        public string ColumnNamePinYinFirstLetter
        {
            get
            {
                if (string.IsNullOrEmpty(_columnNamePinYinFirstLetter))
                {
                    _columnNamePinYinFirstLetter = JPinYin.GetFirst(Name);
                }
                return _columnNamePinYinFirstLetter;
            }
            set
            {
                _columnNamePinYinFirstLetter = value;
            }
        }
        /// <summary>实体名称</summary>
        public string EntityName
        {
            get
            {
                if (_entityName.IsNullOrEmpty())
                {
                    _entityName = Name;
                }
                return _entityName;
            }
            set
            {
                _entityName = value;
            }
        }

        /// <summary>C# 类型,读取配置文件匹配</summary>
        public string CSharpType { get; set; }

    }
}