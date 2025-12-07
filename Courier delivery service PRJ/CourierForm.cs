using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Courier_delivery_service_PRJ
{
    public partial class CourierForm : Form
    {
        public Form1 form1 { get; set; }
        public int courier_id { get; set; }
        public decimal balance = 0;
        private string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        private System.Threading.Timer orderGeneratorTimer;
        private AddressService addressService;

        public CourierForm(Form1 form, int courierId)
        {
            form1 = form;
            courier_id = courierId;
            InitializeComponent();
            LoadNewOrders();
            updateBalance();

            addressService = new AddressService();
            StartOrderGenerator();
        }
        public CourierForm()
        {
            InitializeComponent();
            LoadNewOrders();
            updateBalance();

            addressService = new AddressService();
            StartOrderGenerator();
        }

        private void StartOrderGenerator()
        {
            orderGeneratorTimer = new System.Threading.Timer(
                async _ => await GenerateRandomOrderAsync(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromMinutes(1)
            );
        }

        private async Task GenerateRandomOrderAsync()
        {
            try
            {
                string toAddress = await addressService.GetRandomRussianAddress();

                await InsertRandomOrder(toAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка генерации заказа: {ex.Message}");
            }
        }

        private async Task InsertRandomOrder(string toAddress)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    await conn.OpenAsync();

                    int randomClientId = await GetRandomClientId(conn);
                    string phone = null;

                    string phoneQuery = @"SELECT client_phone FROM clients WHERE client_id = @ClientId";

                    using (SqlCommand command = new SqlCommand(phoneQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ClientId", randomClientId);

                        phone = Convert.ToString(await command.ExecuteScalarAsync());
                    }

                    int randomProductId = await GetRandomProductId(conn);
                    string fromAddress = null;
                    decimal price = 0;

                    string fromAddressANDpriceQuery = @"SELECT address, price FROM products WHERE product_id = @ProductId";
                    
                    using (SqlCommand command = new SqlCommand(fromAddressANDpriceQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ProductId", randomProductId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                fromAddress = Convert.ToString(reader["address"]);
                                price = Convert.ToDecimal(reader["price"]);
                            }
                        }
                    }

                    int orderId = 1;
                    string insertOrdersQuery = @"
                INSERT INTO orders (client_id, courier_id, from_address, to_address, recipient_phone, price, order_status, created_date)
                VALUES (@ClientId, NULL, @FromAddress, @ToAddress, @RecipientPhone, @Price, 'new', GETDATE());
                SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(insertOrdersQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ClientId", randomClientId);
                        command.Parameters.AddWithValue("@FromAddress", fromAddress);
                        command.Parameters.AddWithValue("@ToAddress", toAddress);
                        command.Parameters.AddWithValue("@RecipientPhone", phone);
                        command.Parameters.AddWithValue("@Price", price);

                        orderId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    }

                    string insertHistoryOrdersQuery = @"
                INSERT INTO order_status_history (order_id, courier_id, order_status, status_date)
                VALUES (@OrderId, NULL, 'new', GETDATE())";

                    using (SqlCommand command = new SqlCommand(insertHistoryOrdersQuery, conn))
                    {
                        command.Parameters.AddWithValue("@OrderId", orderId);

                        await command.ExecuteNonQueryAsync();
                    }

                    string insertCl_AcQuery = @"
                    INSERT INTO client_actions (client_id, order_id, client_action, action_date, details)
                    VALUES (@ClientId, @OrderId, 'create_order', GETDATE(), 'Заказ создан')";
                    
                    using (SqlCommand command = new SqlCommand(insertCl_AcQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ClientId", randomClientId);
                        command.Parameters.AddWithValue("@OrderId", orderId);

                        await command.ExecuteNonQueryAsync();
                    }

                    Random rand = new Random();
                    string[] methods = { "cash", "card", "online" };
                    string method = methods[rand.Next(methods.Length)];
                    string paymentsQuery = @"
                    INSERT INTO payments (order_id, amount, method, payment_status, payment_date)
                    VALUES (@OrderId, @Amount, @Method, 'waiting', GETDATE())";

                    using (SqlCommand command = new SqlCommand(paymentsQuery, conn))
                    {
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@Amount", price);
                        command.Parameters.AddWithValue("@Method", method);

                        await command.ExecuteNonQueryAsync();
                    }
                    //MessageBox.Show($"УСПЕШНАЯ ВСТАВКА ЗАКАЗА №{orderId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка вставки заказа: {ex.Message}");
            }
        }
        private async Task<int> GetRandomClientId(SqlConnection conn)
        {
            string query = "SELECT client_id FROM clients";
            var clientIds = new List<int>();

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        clientIds.Add((int)reader["client_id"]);
                    }
                }
            }

            if (clientIds.Count > 0)
            {
                var random = new Random();
                return clientIds[random.Next(clientIds.Count)];
            }

            return 1;
        }
        private async Task<int> GetRandomProductId(SqlConnection conn)
        {
            string query = "SELECT product_id FROM products";
            var productIds = new List<int>();

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productIds.Add((int)reader["product_id"]);
                    }
                }
            }

            if (productIds.Count > 0)
            {
                var random = new Random();
                return productIds[random.Next(productIds.Count)];
            }

            return 1;
        }

        private void LoadNewOrders()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            o.order_id,
                            o.from_address,
                            o.to_address,
                            o.recipient_phone,
                            o.price,
                            o.created_date,
                            c.client_name,
                            c.client_phone
                        FROM orders o
                        INNER JOIN clients c ON o.client_id = c.client_id
                        WHERE o.order_status = 'new'
                        ORDER BY o.created_date ASC";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable ordersTable = new DataTable();
                            adapter.Fill(ordersTable);

                            DisplayOrders(ordersTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayOrders(DataTable ordersTable)
        {
            flowLayoutPanelOrders.Controls.Clear();

            foreach (DataRow order in ordersTable.Rows)
            {
                Panel orderCard = CreateOrderCard(order);
                flowLayoutPanelOrders.Controls.Add(orderCard);
            }

            if (ordersTable.Rows.Count == 0)
            {
                Label noOrdersLabel = new Label
                {
                    Text = "Нет новых заказов для принятия",
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(400, 50),
                    Dock = DockStyle.Fill
                };
                flowLayoutPanelOrders.Controls.Add(noOrdersLabel);
            }
        }

        private Panel CreateOrderCard(DataRow order)
        {
            Panel card = new Panel
            {
                Size = new Size(350, 280),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(10),
                Tag = order["order_id"]
            };

            Label titleLabel = new Label
            {
                Text = $"ЗАКАЗ №{order["order_id"]}",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(10, 10),
                Size = new Size(330, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label clientLabel = new Label
            {
                Text = $"Клиент: {order["client_name"]}",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(10, 45),
                Size = new Size(330, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label phoneLabel = new Label
            {
                Text = $"Тел: {order["client_phone"]}",
                Font = new Font("Arial", 9),
                Location = new Point(10, 65),
                Size = new Size(330, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label fromLabel = new Label
            {
                Text = "Откуда:",
                Font = new Font("Arial", 9, FontStyle.Bold),
                Location = new Point(10, 90),
                Size = new Size(330, 15),
                TextAlign = ContentAlignment.MiddleLeft
            };

            TextBox fromAddressBox = new TextBox
            {
                Text = order["from_address"].ToString(),
                Location = new Point(10, 105),
                Size = new Size(330, 40),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
                Font = new Font("Arial", 8)
            };

            Label toLabel = new Label
            {
                Text = "Куда:",
                Font = new Font("Arial", 9, FontStyle.Bold),
                Location = new Point(10, 150),
                Size = new Size(330, 15),
                TextAlign = ContentAlignment.MiddleLeft
            };

            TextBox toAddressBox = new TextBox
            {
                Text = order["to_address"].ToString(),
                Location = new Point(10, 165),
                Size = new Size(330, 40),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
                Font = new Font("Arial", 8)
            };

            Label recipientLabel = new Label
            {
                Text = $"Получатель: {order["recipient_phone"]}",
                Font = new Font("Arial", 9),
                Location = new Point(10, 210),
                Size = new Size(330, 15),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label priceLabel = new Label
            {
                Text = $"Стоимость: {Convert.ToDecimal(order["price"]):C}",
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.Green,
                Location = new Point(10, 225),
                Size = new Size(160, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label dateLabel = new Label
            {
                Text = $"Создан: {Convert.ToDateTime(order["created_date"]):dd.MM.yy HH:mm:ss}",
                Font = new Font("Arial", 8),
                ForeColor = Color.Gray,
                Location = new Point(180, 225),
                Size = new Size(160, 20),
                TextAlign = ContentAlignment.MiddleRight
            };

            Button acceptButton = new Button
            {
                Text = "ПРИНЯТЬ ЗАКАЗ",
                Location = new Point(10, 245),
                Size = new Size(330, 25),
                BackColor = Color.ForestGreen,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Tag = order["order_id"]
            };
            acceptButton.FlatAppearance.BorderSize = 0;
            acceptButton.Click += AcceptOrderButton_Click;

            acceptButton.MouseEnter += (s, e) => { acceptButton.BackColor = Color.LimeGreen; };
            acceptButton.MouseLeave += (s, e) => { acceptButton.BackColor = Color.ForestGreen;  };

            card.MouseEnter += (s, e) => { card.BackColor = Color.LightCyan; fromAddressBox.BackColor = Color.LightCyan; toAddressBox.BackColor = Color.LightCyan; };
            card.MouseLeave += (s, e) => { card.BackColor = Color.White; fromAddressBox.BackColor = Color.White; toAddressBox.BackColor = Color.White; };
            fromAddressBox.MouseEnter += (s, e) => { card.BackColor = Color.LightCyan; fromAddressBox.BackColor = Color.LightCyan; toAddressBox.BackColor = Color.LightCyan; };
            fromAddressBox.MouseLeave += (s, e) => { card.BackColor = Color.White; fromAddressBox.BackColor = Color.White; toAddressBox.BackColor = Color.White; };
            toAddressBox.MouseEnter += (s, e) => { card.BackColor = Color.LightCyan; fromAddressBox.BackColor = Color.LightCyan; toAddressBox.BackColor = Color.LightCyan; };
            toAddressBox.MouseLeave += (s, e) => { card.BackColor = Color.White; fromAddressBox.BackColor = Color.White; toAddressBox.BackColor = Color.White; };

            card.Controls.AddRange(new Control[] {
                titleLabel, clientLabel, phoneLabel, fromLabel, fromAddressBox,
                toLabel, toAddressBox, recipientLabel, priceLabel, dateLabel, acceptButton
            });

            return card;
        }

        private void AcceptOrderButton_Click(object sender, EventArgs e)
        {
            Button acceptButton = (Button)sender;
            int orderId = (int)acceptButton.Tag;
            CourierNotes courierNotes = new CourierNotes();
            courierNotes.form1 = form1;
            courierNotes.courier = this;
            courierNotes.OrderId = orderId;
            this.Hide();
            courierNotes.Show();
        }

        public async void CompleteOrderAcceptance(int orderId, string notes)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    await conn.OpenAsync();

                    string updateQuery = @"UPDATE orders SET order_status = 'assigned', courier_id = @CourierId WHERE order_id = @OrderId";

                    using (SqlCommand command = new SqlCommand(updateQuery, conn))
                    {
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@CourierId", courier_id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            string busyCourierQuery = @"UPDATE couriers SET courier_status = 'busy' WHERE courier_id = @CourierId";
                            using (SqlCommand command2 = new SqlCommand(busyCourierQuery, conn))
                            {
                                command2.Parameters.AddWithValue("@CourierId", courier_id);
                                await command2.ExecuteNonQueryAsync();
                            }
                            AddCourierAction(orderId, "accept_order", notes);

                            CreateOrderStatusHistory(orderId, "assigned");

                            MessageBox.Show("Заказ успешно принят!", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            DeliveryProcessForm deliveryForm = new DeliveryProcessForm(orderId, courier_id, this);
                            deliveryForm.Show();
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка принятия заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
            }
        }

        private void CreateOrderStatusHistory(int orderId, string status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"INSERT INTO order_status_history (order_id, courier_id, order_status)
                           VALUES (@order_id, @courier_id, @status)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@order_id", orderId);
                        cmd.Parameters.AddWithValue("@courier_id", courier_id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания истории статуса: {ex.Message}");
            }
        }

        private void AddCourierAction(int orderId, string action, string notes)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO courier_actions (courier_id, order_id, courier_action, notes)
                        VALUES (@CourierId, @OrderId, @Action, @Notes)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@CourierId", courier_id);
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@Action", action);
                        command.Parameters.AddWithValue("@Notes", notes);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи действия курьера: {ex.Message}");
            }
        }

        private void CourierForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            orderGeneratorTimer?.Dispose();
            form1?.Show();
        }

        public void RefreshOrders()
        {
            LoadNewOrders();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            form1?.Show();
        }

        private void refreshOrdersButton_Click(object sender, EventArgs e)
        {
            RefreshOrders();
        }

        public void updateBalance()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = @"SELECT courier_balance FROM courier_balances WHERE courier_id = @CourierId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourierId", courier_id);
                        balance = Convert.ToDecimal(command.ExecuteScalar());
                    }
                    balanceLabel.Text = $"Баланс: {balance}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки баланса: {ex.Message}");
            }
        }

        private void CourierForm_Load(object sender, EventArgs e)
        {
            updateBalance();
        }

        private void sendMoneyButton_Click(object sender, EventArgs e)
        {
            SendMoneyForm sendMoneyForm = new SendMoneyForm(this, courier_id, balance);
            sendMoneyForm.Show();
            this.Hide();
        }

        private void supportButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = @"INSERT INTO courier_actions (courier_id, order_id, courier_action, notes)
                VALUES (@crId, NULL, 'contact_support', 'Обратился в службу поддержки')";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@crId", courier_id);

                        cmd.ExecuteNonQuery();
                    }
                }
                Support support = new Support(this);
                support.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обращении в службу поддержки: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}