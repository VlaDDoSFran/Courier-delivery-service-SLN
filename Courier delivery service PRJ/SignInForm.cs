using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Courier_delivery_service_PRJ
{
    public partial class SignInForm : Form
    {
        public Form1 form1 { get; set; }
        string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        public SignInForm()
        {
            InitializeComponent();
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Multiline = false;
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;
            if (login.Length == 0 || password.Length == 0 || ChooseComboBox.Text.Length == 0)
            {
                DialogResult messageBox = MessageBox.Show("Не все поля заполнены.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (ChooseComboBox.Text == "Пользователь")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM client_auth WHERE (client_name = @l OR client_phone = @l) AND client_password = @p";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@l", login);
                    cmd.Parameters.AddWithValue("@p", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        string query2 = "SELECT is_active FROM client_auth WHERE (client_name = @l OR client_phone = @l) AND client_password = @p";
                        string query3 = "SELECT client_id FROM client_auth WHERE (client_name = @l OR client_phone = @l) AND client_password = @p";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@l", login);
                        cmd2.Parameters.AddWithValue("@p", password);
                        int is_active = Convert.ToInt32(cmd2.ExecuteScalar());
                        if (is_active == 0)
                        {
                            DialogResult messageBox = MessageBox.Show("Ваш аккаунт заблокирован!", "BAN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            cmd2 = new SqlCommand(query3, conn);
                            cmd2.Parameters.AddWithValue("@l", login);
                            cmd2.Parameters.AddWithValue("@p", password);
                            ClientForm clientForm = new ClientForm(form1, Convert.ToInt32(cmd2.ExecuteScalar()));

                            string updateLoginQuery = "UPDATE client_auth SET last_login = GETDATE() WHERE (client_name = @l OR client_phone = @l)";
                            using (SqlCommand updateCmd = new SqlCommand(updateLoginQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@l", login);
                                updateCmd.ExecuteNonQuery();
                            }

                            clientForm.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        DialogResult messageBox = MessageBox.Show("Неверный логин или пароль!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (ChooseComboBox.Text == "Курьер")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM courier_auth WHERE (courier_name = @l OR courier_phone = @l) AND courier_password = @p";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@l", login);
                    cmd.Parameters.AddWithValue("@p", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        string query2 = "SELECT is_active FROM courier_auth WHERE (courier_name = @l OR courier_phone = @l) AND courier_password = @p";
                        string query3 = "SELECT courier_id FROM courier_auth WHERE (courier_name = @l OR courier_phone = @l) AND courier_password = @p";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@l", login);
                        cmd2.Parameters.AddWithValue("@p", password);
                        int is_active = Convert.ToInt32(cmd2.ExecuteScalar());
                        if (is_active == 0)
                        {
                            DialogResult messageBox = MessageBox.Show("Ваш аккаунт заблокирован!", "BAN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            cmd2 = new SqlCommand(query3, conn);
                            cmd2.Parameters.AddWithValue("@l", login);
                            cmd2.Parameters.AddWithValue("@p", password);
                            CourierForm courierForm = new CourierForm(form1, Convert.ToInt32(cmd2.ExecuteScalar()));

                            string updateLoginQuery = "UPDATE courier_auth SET last_login = GETDATE() WHERE (courier_name = @l OR courier_phone = @l)";
                            using (SqlCommand updateCmd = new SqlCommand(updateLoginQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@l", login);
                                updateCmd.ExecuteNonQuery();
                            }

                            courierForm.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        DialogResult messageBox = MessageBox.Show("Неверный логин или пароль!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (ChooseComboBox.Text == "Администратор")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM admin_auth WHERE (admin_name = @l OR admin_phone = @l) AND admin_password = @p";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@l", login);
                    cmd.Parameters.AddWithValue("@p", password);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        string query2 = "SELECT is_active FROM admin_auth WHERE (admin_name = @l OR admin_phone = @l) AND admin_password = @p";
                        string query3 = "SELECT admin_id FROM admin_auth WHERE (admin_name = @l OR admin_phone = @l) AND admin_password = @p";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@l", login);
                        cmd2.Parameters.AddWithValue("@p", password);
                        int is_active = Convert.ToInt32(cmd2.ExecuteScalar());
                        if (is_active == 0)
                        {
                            DialogResult messageBox = MessageBox.Show("Ваш аккаунт заблокирован!", "BAN", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            cmd2 = new SqlCommand(query3, conn);
                            cmd2.Parameters.AddWithValue("@l", login);
                            cmd2.Parameters.AddWithValue("@p", password);
                            AdminForm adminForm = new AdminForm(form1, Convert.ToInt32(cmd2.ExecuteScalar()));

                            string updateLoginQuery = "UPDATE admin_auth SET last_login = GETDATE() WHERE (admin_name = @l OR admin_phone = @l)";
                            using (SqlCommand updateCmd = new SqlCommand(updateLoginQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@l", login);
                                updateCmd.ExecuteNonQuery();
                            }

                            adminForm.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        DialogResult messageBox = MessageBox.Show("Неверный логин или пароль!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void returnBackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            form1.Show();
        }

        private void SignInForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.Close();
        }

        private void ChooseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChooseComboBox.Text == "Администратор")
            {
                adminWarningLabel.Visible = true;
            }
            else
            {
                adminWarningLabel.Visible = false;
            }
        }

        private void showPasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            PasswordTextBox.PasswordChar = '\0';
            PasswordTextBox.Multiline = true;
        }

        private void showPasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Multiline = false;
        }
    }
}
