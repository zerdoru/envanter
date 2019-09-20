using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Envanter
{
    public partial class Login : Form
    {
        private readonly SqlConnection _connection =
            new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bilgiislem\source\repos\envanter\Envanter\Envanter\Inventory.mdf;Integrated Security=True");

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var dt = ConnectionUtils.ExecuteCommand(_connection,
                $"SELECT * FROM Registration WHERE Username='{textBoxUsername.Text}' AND Password='{textBoxPassword.Text}'");

            if (dt.Rows.Count > 0)
            {
                Hide();
                var mdi = new MDIParent1();
                mdi.Show();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}