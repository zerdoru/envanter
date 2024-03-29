﻿using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Envanter
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            ListUsersToGrid();
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionUtils.ExecuteCommand(
                    $"INSERT INTO Registration (Username, Password, Email, FirstName, LastName) VALUES ('{textBoxUsername.Text}', '{textBoxPassword.Text}', '{textBoxEmail.Text}', '{textBoxFirstName.Text}', '{textBoxLastName.Text}')");
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
                if (control is TextBox)
                    control.Text = "";

            ShowStatusLabel("User successfully added");

            ListUsersToGrid();
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            var id = dataGridView1.SelectedCells[0].Value.ToString();

            var dt = ConnectionUtils.ExecuteCommand($"DELETE FROM Registration WHERE Id='{id}'");
            dataGridView1.DataSource = dt;
            ListUsersToGrid();
            ShowStatusLabel($"User with id {id} was deleted");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ListUsersToGrid()
        {
            dataGridView1.DataSource = ConnectionUtils.ExecuteCommand("SELECT * FROM Registration");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelStatus.Visible = false;
            timer1.Stop();
        }

        private void ShowStatusLabel(string text)
        {
            labelStatus.Visible = true;
            labelStatus.Text = text;
            timer1.Start();
        }
    }
}