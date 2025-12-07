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
    public partial class Ordering : Form
    {
        public Form1 form1 { get; set; }
        public UserOrdersForm u_orders { get; set; }
        public ClientForm clientForm { get; set; }
        public int client_id { get; set; }
        public int productId { get; set; }
        string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        public Ordering()
        {
            InitializeComponent();

            paymentComboBox.Items.AddRange(new string[] { "card", "cash", "online" });
            paymentComboBox.SelectedIndex = 0;
        }

        public void LoadProductInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT client_phone FROM clients WHERE client_id = @client_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@client_id", client_id);
                        phoneTextBox.Text = (string)cmd.ExecuteScalar();
                    }
                }
                if (productId == 0)
                {
                    productInfoLabel.Text = "Ошибка: productId = 0";
                    return;
                }
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT product_name, price, address FROM products WHERE product_id = @productId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string productName = reader["product_name"].ToString();
                                decimal price = Convert.ToDecimal(reader["price"]);
                                string fromAddress = reader["address"].ToString();

                                productInfoLabel.Text = $"Товар: {productName}\nЦена: {price:C}\nМагазин: {fromAddress}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки информации о товаре: {ex.Message}");
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("Введите адрес доставки!");
                return;
            }

            if (string.IsNullOrEmpty(phoneTextBox.Text))
            {
                MessageBox.Show("Введите корректный номер телефона!");
                return;
            }

            CreateOrder();
        }

        private void CreateOrder()
        {
            try
            {
                int orderId = 0;
                decimal price = 0;
                string productName = "";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string productQuery = "SELECT product_name, price, address FROM products WHERE product_id = @productId";
                    string fromAddress = "";

                    using (SqlCommand productCmd = new SqlCommand(productQuery, conn))
                    {
                        productCmd.Parameters.AddWithValue("@productId", productId);
                        using (SqlDataReader reader = productCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                productName = reader["product_name"].ToString();
                                price = Convert.ToDecimal(reader["price"]);
                                fromAddress = reader["address"].ToString();
                            }
                        }
                    }

                    string orderQuery = @"
                INSERT INTO orders (client_id, from_address, to_address, recipient_phone, price, order_status)
                VALUES (@clientId, @fromAddress, @toAddress, @recipientPhone, @price, 'new');
                SELECT SCOPE_IDENTITY();";

                    using (SqlCommand orderCmd = new SqlCommand(orderQuery, conn))
                    {
                        orderCmd.Parameters.AddWithValue("@clientId", client_id);
                        orderCmd.Parameters.AddWithValue("@fromAddress", fromAddress);
                        orderCmd.Parameters.AddWithValue("@toAddress", addressTextBox.Text);
                        orderCmd.Parameters.AddWithValue("@recipientPhone", phoneTextBox.Text);
                        orderCmd.Parameters.AddWithValue("@price", price);
                        orderId = Convert.ToInt32(orderCmd.ExecuteScalar());
                    }

                    string paymentQuery = @"
                INSERT INTO payments (order_id, amount, method, payment_status)
                VALUES (@orderId, @amount, @method, 'waiting')";

                    using (SqlCommand paymentCmd = new SqlCommand(paymentQuery, conn))
                    {
                        paymentCmd.Parameters.AddWithValue("@orderId", orderId);
                        paymentCmd.Parameters.AddWithValue("@amount", price);
                        paymentCmd.Parameters.AddWithValue("@method", paymentComboBox.SelectedItem.ToString());
                        paymentCmd.ExecuteNonQuery();
                    }

                    string actionQuery = @"
                INSERT INTO client_actions (client_id, order_id, client_action, details)
                VALUES (@clientId, @orderId, 'create_order', @details)";

                    using (SqlCommand actionCmd = new SqlCommand(actionQuery, conn))
                    {
                        actionCmd.Parameters.AddWithValue("@clientId", client_id);
                        actionCmd.Parameters.AddWithValue("@orderId", orderId);
                        actionCmd.Parameters.AddWithValue("@details", $"Создан заказ на товар: {productName}");
                        actionCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    $"✅ Заказ №{orderId} успешно создан!\n\n" +
                    $"📦 Товар: {productName}\n" +
                    $"💰 Сумма: {price:C}\n" +
                    $"🏠 Адрес доставки: {addressTextBox.Text}\n" +
                    $"📞 Телефон: {phoneTextBox.Text}\n" +
                    $"💳 Способ оплаты: {paymentComboBox.SelectedItem}",
                    "Заказ оформлен",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                UserOrdersForm ordersForm = new UserOrdersForm(client_id, orderId, price, paymentComboBox.Text, form1, clientForm);
                ordersForm.Show();
                this.Hide();

                //returnBackButton.PerformClick();

                addressTextBox.Text = "";
                phoneTextBox.Text = "";
                clientDetailsTextBox.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void returnBackButton_Click(object sender, EventArgs e)
        {
            clientForm?.Show();
            this.Close();
        }

        private void Ordering_FormClosing(object sender, FormClosingEventArgs e)
        {
            productInfoLabel.Text = "";
            addressTextBox.Text = "";
            phoneTextBox.Text = "";
        }

        private void Ordering_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientForm?.Show();
        }
    }
}
