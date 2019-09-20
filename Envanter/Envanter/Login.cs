using System;
using System.Windows.Forms;

namespace Envanter
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var dt = ConnectionUtils.ExecuteCommand(
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