using System.Data;
using System.Data.SqlClient;

namespace Envanter
{
    public static class ConnectionUtils
    {
        private static SqlConnection _connection;

        private static void ValidateConnection()
        {
            if (_connection != null) return;

            _connection =
                new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bilgiislem\source\repos\envanter\Envanter\Envanter\Inventory.mdf;Integrated Security=True");

            if (_connection.State != ConnectionState.Open) _connection.Open();
        }

        public static DataTable ExecuteCommand(string commandText)
        {
            ValidateConnection();

            var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = commandText;
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }
    }
}