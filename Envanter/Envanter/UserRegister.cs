using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Envanter
{
    public partial class UserRegister : Form
    {
        private readonly SqlConnection _connection =
            new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bilgiislem\source\repos\envanter\Envanter\Envanter\Inventory.mdf;Integrated Security=True");

        public UserRegister()
        {
            InitializeComponent();
        }

        private void UserRegister_Load(object sender, EventArgs e)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText =
                $"INSERT INTO Registration (Username, Password, Email, FirstName, LastName) VALUES ('{textBoxUsername.Text}', '{textBoxPassword.Text}', '{textBoxEmail.Text}', '{textBoxFirstName.Text}', '{textBoxLastName.Text}')";

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                switch (exception.Number)
                {
                    case 2601:
                    case 2627:
                        MessageBox.Show("The e-mail address or username you entered is already in use");
                        break;
                    default:
                        MessageBox.Show("Error while saving data");
                        break;
                }

                return;
            }

            MessageBox.Show("Registration successfull");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}