using System.Data;

namespace Serilog.Sinks.MSSqlServer
{
    internal class ColumnOption
    {
        public string ColumnName { get; set; }
        public SqlDbType DataType { get; set; }
    }
}