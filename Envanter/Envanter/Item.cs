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
            ListItemsToGrid();
        }

        private void ButtonAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionUtils.ExecuteCommand(
                    $"INSERT INTO Item (Product, Count) VALUES ('{textBoxProduct.Text}', {numericUpDownCount.Value})");
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
                        MessageBox.Show($"Error while saving data\n\n{exception.Message}");
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

            AppendStatusText("Product successfully added");

            ListItemsToGrid();
        }

        private void buttonDeleteProduct_Click(object sender, EventArgs e)
        {
            var cells = dataGridView1.SelectedCells;
            if (cells.Count < 1) return;

            var id = dataGridView1.SelectedCells[0].Value.ToString();

            var dt = ConnectionUtils.ExecuteCommand($"DELETE FROM Item WHERE Id='{id}'");
            dataGridView1.DataSource = dt;
            ListItemsToGrid();
            AppendStatusText($"Item with id {id} was deleted");
        }

        private void ListItemsToGrid()
        {
            dataGridView1.DataSource = ConnectionUtils.ExecuteCommand("SELECT * FROM Item");
        }

        private void AppendStatusText(string text)
        {
            textBoxStatus.Text += $"{DateTime.Now.ToString("HH:mm:ss")}: {text}\n";
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}