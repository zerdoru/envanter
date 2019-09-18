using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Envanter
{
    public partial class Login : Form
    {
        SqlConnection _connection =
            new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bilgiislem\source\repos\envanter\Envanter\Envanter\Inventory.mdf;Integrated Security=True");

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText =
                $"SELECT * FROM Registration WHERE Username='{textBoxUsername.Text}' AND Password='{textBoxPassword.Text}'";
            cmd.ExecuteNonQuery();
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Connected");
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }
    }
}