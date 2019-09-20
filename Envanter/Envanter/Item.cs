using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Envanter
{
    public partial class Item : Form
    {
        public Item()
        {
            InitializeComponent();
        }

        private void Item_Load(object sender, EventArgs e)
        {
            ListUsersToGrid();
        }

        private void ButtonAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionUtils.ExecuteCommand(
                    $"INSERT INTO Item (Product, Count) VALUES ('{textBoxProduct.Text}', {numericUpDownCount})");
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

            foreach (Control control in panel1.Controls)
                switch (control)
                {
                    case TextBox _:
                        control.Text = "";
                        break;
                    case NumericUpDown upDown:
                        upDown.Value = 1;
                        break;
                }

            ShowStatusLabel("Product successfully added");

            ListUsersToGrid();
        }

        private void ListUsersToGrid()
        {
            dataGridView1.DataSource = ConnectionUtils.ExecuteCommand("SELECT * FROM Item");
        }

        private void ShowStatusLabel(string text)
        {
            labelStatus.Visible = true;
            labelStatus.Text = text;
            timer1.Start();
        }
    }
}