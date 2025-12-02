using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Courier_delivery_service_PRJ
{
    public partial class SignUpForm : Form
    {
        public Form1 form1 { get; set; }
        string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        public SignUpForm()
        {
            InitializeComponent();
            EmailORvehicleLabel.Text = "";
            ChooseComboBox.Text = "";
            EmailORvehicleTextBox.Enabled = false;
        }

        private void returnBackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            form1.Show();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string login = LoginTextBox.Text;
            string phone = PhoneTextBox.Text;
            string emailORvehicle = EmailORvehicleTextBox.Text;
            string password = PasswordTextBox.Text;
            if (login.Length == 0 || phone.Length == 0 || emailORvehicle.Length == 0 || password.Length == 0 || ChooseComboBox.Text.Length == 0)
            {
                DialogResult messageBox = MessageBox.Show("Не все поля заполнены.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (ChooseComboBox.Text == "Пользователь")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM client_auth WHERE (client_name = @l OR client_phone = @p)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@l", login);
                    cmd.Parameters.AddWithValue("@p", phone);
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string query2 = "INSERT INTO clients(client_name, client_phone, email) VALUES (@n, @p, @e)";
                        string getId = "SELECT TOP 1 client_id FROM clients ORDER BY client_id DESC";
                        string query3 = "INSERT INTO client_auth(client_id, client_name, client_phone, client_password) VALUES (@id, @n, @p, @pw)";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@n", login);
                        cmd2.Parameters.AddWithValue("@p", phone);
                        cmd2.Parameters.AddWithValue("@e", emailORvehicle);
                        cmd2.ExecuteNonQuery();
                        SqlCommand cmdGetId = new SqlCommand(getId, conn);
                        int client_id = (int)cmdGetId.ExecuteScalar();
                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.Parameters.AddWithValue("@id", client_id);
                        cmd3.Parameters.AddWithValue("@n", login);
                        cmd3.Parameters.AddWithValue("@p", phone);
                        cmd3.Parameters.AddWithValue("@pw", password);
                        cmd3.ExecuteNonQuery();
                        DialogResult messageBox = MessageBox.Show("Вы успешно зарегистрировались!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogResult messageBox = MessageBox.Show("Пользователь с таким именем и фамилией (телефоном) уже существует!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }
            else if (ChooseComboBox.Text == "Курьер")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM courier_auth WHERE (courier_name = @l OR courier_phone = @p)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@l", login);
                    cmd.Parameters.AddWithValue("@p", phone);
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string query2 = "INSERT INTO couriers(courier_name, courier_phone, vehicle) VALUES (@n, @p, @v)";
                        string getId = "SELECT TOP 1 courier_id FROM couriers ORDER BY courier_id DESC";
                        string query3 = "INSERT INTO courier_auth(courier_id, courier_name, courier_phone, courier_password) VALUES (@id, @n, @p, @pw)";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@n", login);
                        cmd2.Parameters.AddWithValue("@p", phone);
                        cmd2.Parameters.AddWithValue("@v", emailORvehicle);
                        cmd2.ExecuteNonQuery();
                        SqlCommand cmdGetId = new SqlCommand(getId, conn);
                        int courier_id = (int)cmdGetId.ExecuteScalar();
                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.Parameters.AddWithValue("@id", courier_id);
                        cmd3.Parameters.AddWithValue("@n", login);
                        cmd3.Parameters.AddWithValue("@p", phone);
                        cmd3.Parameters.AddWithValue("@pw", password);
                        cmd3.ExecuteNonQuery();
                        DialogResult messageBox = MessageBox.Show("Вы успешно зарегистрировались!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogResult messageBox = MessageBox.Show("Курьер с таким именем и фамилией (телефоном) уже существует!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }
        }

        private void ChooseComboBox_TextChanged(object sender, EventArgs e)
        {
            EmailORvehicleLabel.Text = "";
            EmailORvehicleTextBox.Text = "";
            EmailORvehicleTextBox.Enabled = false;
            if (ChooseComboBox.Text == "Пользователь")
            {
                EmailORvehicleTextBox.Enabled = true;
                EmailORvehicleLabel.Text = "Введите ваш адрес электронной почты:";
            }
            else if (ChooseComboBox.Text == "Курьер")
            {
                EmailORvehicleTextBox.Enabled = true;
                EmailORvehicleLabel.Text = "Введите название вашего транспорта:";
            }
        }

        private void SignUpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.Close();
        }
    }
}
