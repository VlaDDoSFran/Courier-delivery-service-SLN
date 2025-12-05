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
    public partial class SendMoneyForm : Form
    {
        private string Table;
        private string Table2;
        private string Id;
        private string Balance;
        private int userId;
        private decimal userBalance;
        public Form form;
        string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        public SendMoneyForm(Form form, int Id, decimal balance)
        {
            InitializeComponent();

            if (form is ClientForm)
            {
                this.form = (ClientForm)form;
                Table = "client_balances";
                Table2 = "client_transactions";
                this.Id = "client_id";
                Balance = "client_balance";
            }
            else if (form is CourierForm)
            {
                this.form = (CourierForm)form;
                Table = "courier_balances";
                Table2 = "courier_transactions";
                this.Id = "courier_id";
                Balance = "courier_balance";
            }
            userId = Id;
            userBalance = balance;
        }

        private void SendMoneyButton_Click(object sender, EventArgs e)
        {
            try
            {
                string login = LoginTextBox.Text;
                string quantity = QuantityTextBox.Text;
                if (login.Length == 0 || quantity.Length == 0 || Convert.ToDecimal(quantity) <= 0 || ChooseComboBox.Text.Length == 0)
                {
                    MessageBox.Show("Не все поля заполнены. (Или сумма перевода меньше или равна нулю)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (userBalance < Convert.ToDecimal(quantity))
                {
                    MessageBox.Show("Недостаточно средств.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else if (ChooseComboBox.Text == "Пользователь")
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string subQuery = $@"UPDATE {Table} SET {Balance} = @balance WHERE {Id} = @Id";
                        using (SqlCommand cmd = new SqlCommand(subQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@balance", userBalance - Convert.ToDecimal(quantity));
                            cmd.Parameters.AddWithValue("@Id", userId);

                            cmd.ExecuteNonQuery();
                        }

                        string getUserIdQuery = @"SELECT client_id FROM clients WHERE client_name = @cl OR client_phone = @cl OR client_id = @cl";
                        int receivedUserId = 1;
                        string beforeAddQuery = @"SELECT client_balance FROM client_balances WHERE client_id = @Id";
                        decimal receivedUserBalance = 0;
                        string addQuery = @"UPDATE client_balances SET client_balance = @balance WHERE client_id = @Id";
                        using (SqlCommand cmd = new SqlCommand(getUserIdQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@cl", login);

                            receivedUserId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        using (SqlCommand cmd = new SqlCommand(beforeAddQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", receivedUserId);

                            receivedUserBalance = Convert.ToDecimal(cmd.ExecuteScalar());
                        }
                        using (SqlCommand cmd = new SqlCommand(addQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@balance", receivedUserBalance + Convert.ToDecimal(quantity));
                            cmd.Parameters.AddWithValue("@Id", receivedUserId);

                            cmd.ExecuteNonQuery();
                        }

                        string updTransactions = $@"
                    INSERT INTO {Table2} ({Id}, userName, userId, quantity, notes)
                    VALUES (@Id, @userName, @userId, @quantity, @notes)";
                        using (SqlCommand cmd = new SqlCommand(updTransactions, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", userId);
                            cmd.Parameters.AddWithValue("@userName", ChooseComboBox.Text);
                            cmd.Parameters.AddWithValue("@userId", receivedUserId);
                            cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(quantity));
                            cmd.Parameters.AddWithValue("@notes", $"Перевёл {ChooseComboBox.Text} №{receivedUserId} сумму в {Convert.ToDecimal(quantity)} рублей");

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Вы перевели {Convert.ToDecimal(quantity)} рублей {ChooseComboBox.Text} №{receivedUserId}");
                    }
                }
                else if (ChooseComboBox.Text == "Курьер")
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string subQuery = $@"UPDATE {Table} SET {Balance} = @balance WHERE {Id} = @Id";
                        using (SqlCommand cmd = new SqlCommand(subQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@balance", userBalance - Convert.ToDecimal(quantity));
                            cmd.Parameters.AddWithValue("@Id", userId);

                            cmd.ExecuteNonQuery();
                        }

                        string getUserIdQuery = @"SELECT courier_id FROM couriers WHERE courier_name = @cl OR courier_phone = @cl OR courier_id = @cl";
                        int receivedUserId = 1;
                        string beforeAddQuery = @"SELECT courier_balance FROM courier_balances WHERE courier_id = @Id";
                        decimal receivedUserBalance = 0;
                        string addQuery = @"UPDATE courier_balances SET courier_balance = @balance WHERE courier_id = @Id";
                        using (SqlCommand cmd = new SqlCommand(getUserIdQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@cl", login);

                            receivedUserId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        using (SqlCommand cmd = new SqlCommand(beforeAddQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", receivedUserId);

                            receivedUserBalance = Convert.ToDecimal(cmd.ExecuteScalar());
                        }
                        using (SqlCommand cmd = new SqlCommand(addQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@balance", receivedUserBalance + Convert.ToDecimal(quantity));
                            cmd.Parameters.AddWithValue("@Id", receivedUserId);

                            cmd.ExecuteNonQuery();
                        }

                        string updTransactions = $@"
                    INSERT INTO {Table2} ({Id}, userName, userId, quantity, notes)
                    VALUES (@Id, @userName, @userId, @quantity, @notes)";
                        using (SqlCommand cmd = new SqlCommand(updTransactions, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", userId);
                            cmd.Parameters.AddWithValue("@userName", ChooseComboBox.Text);
                            cmd.Parameters.AddWithValue("@userId", receivedUserId);
                            cmd.Parameters.AddWithValue("@quantity", Convert.ToDecimal(quantity));
                            cmd.Parameters.AddWithValue("@notes", $"Перевёл {ChooseComboBox.Text} №{receivedUserId} сумму в {Convert.ToDecimal(quantity)} рублей");

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Вы перевели {Convert.ToDecimal(quantity)} рублей {ChooseComboBox.Text} №{receivedUserId}");
                    }
                }
                this.Hide();
                if (form is ClientForm clientForm)
                {
                    clientForm.updateBalance();
                }
                else if (form is CourierForm courierForm)
                {
                    courierForm.updateBalance();
                }
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка перевода: {ex.Message}");
            }
        }

        private void SendMoneyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Close();
        }
    }
}
