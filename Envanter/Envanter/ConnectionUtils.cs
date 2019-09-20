using System.Data;
using System.Data.SqlClient;

namespace Envanter
{
    public static class ConnectionUtils
    {
        public static DataTable ExecuteCommand(SqlConnection connection, string commandText)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = commandText;
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }
    }
}