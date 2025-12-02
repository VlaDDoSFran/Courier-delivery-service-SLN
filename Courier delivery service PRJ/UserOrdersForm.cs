using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Courier_delivery_service_PRJ
{
    public partial class UserOrdersForm : Form
    {
        private string connStr = @"Data Source=DESKTOP-O03Q1EM;Initial Catalog=Courier_delivery_service;Integrated Security=True";
        public int client_id { get; set; }
        public int order_id { get; set; }
        public decimal order_price { get; set; }
        public string method { get; set; }
        public Form1 form1 { get; set; }
        public ClientForm clientForm { get; set; }

        private Timer animationTimer;
        private int currentStep = 0;
        private Label statusLabel;
        private Panel animationPanel;
        private Button payButton;
        private Label orderInfoLabel;
        private Label emojiLabel;
        private int assignedCourierId = 0;
        private string assignedCourierName = "";
        private string assignedCourierVehicle = "";

        public UserOrdersForm(int clientId, int orderId, decimal price, string meth, Form1 mainForm, ClientForm clForm)
        {
            client_id = clientId;
            order_id = orderId;
            order_price = price;
            form1 = mainForm;
            clientForm = clForm;
            method = meth;

            InitializeCustomComponents();

            orderInfoLabel.Text = $"Заказ №{order_id} • {order_price:C}";
            StartDeliveryAnimation();
        }

        public UserOrdersForm()
        {
            InitializeCustomComponents();

            if (order_id <= 0)
            {
                MessageBox.Show("Ошибка: ID заказа не передан");
                this.Close();
                return;
            }

            orderInfoLabel.Text = $"Заказ №{order_id} • {order_price:C}";
            StartDeliveryAnimation();
        }

        private void InitializeCustomComponents()
        {
            this.Size = new Size(500, 450);
            this.MaximumSize = new Size(500, 450);
            this.MinimumSize = new Size(500, 450);
            this.Text = "Статус доставки";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;

            orderInfoLabel = new Label();
            orderInfoLabel.Size = new Size(450, 30);
            orderInfoLabel.Location = new Point(25, 20);
            orderInfoLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            orderInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            orderInfoLabel.ForeColor = Color.DarkBlue;
            orderInfoLabel.Text = $"Заказ №{order_id} • {order_price:C}";

            animationPanel = new Panel();
            animationPanel.Size = new Size(400, 250);
            animationPanel.Location = new Point(50, 60);
            animationPanel.BackColor = Color.Lavender;
            animationPanel.BorderStyle = BorderStyle.FixedSingle;

            emojiLabel = new Label();
            emojiLabel.Size = new Size(100, 100);
            emojiLabel.Location = new Point(150, 30);
            emojiLabel.Font = new Font("Arial", 48);
            emojiLabel.TextAlign = ContentAlignment.MiddleCenter;

            statusLabel = new Label();
            statusLabel.Size = new Size(380, 80);
            statusLabel.Location = new Point(10, 150);
            statusLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            statusLabel.ForeColor = Color.DarkBlue;

            payButton = new Button();
            payButton.Size = new Size(180, 45);
            payButton.Location = new Point(160, 330);
            payButton.Text = "ОПЛАТИТЬ ЗАКАЗ";
            payButton.BackColor = Color.ForestGreen;
            payButton.ForeColor = Color.White;
            payButton.Font = new Font("Arial", 12, FontStyle.Bold);
            payButton.FlatStyle = FlatStyle.Flat;
            payButton.FlatAppearance.BorderSize = 0;
            payButton.Visible = false;
            payButton.Click += PayButton_Click;

            animationPanel.Controls.Add(emojiLabel);
            animationPanel.Controls.Add(statusLabel);

            this.Controls.Add(orderInfoLabel);
            this.Controls.Add(animationPanel);
            this.Controls.Add(payButton);
        }

        private void StartDeliveryAnimation()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 3000;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            currentStep++;

            switch (currentStep)
            {
                case 1:
                    ShowSearchingCourier();
                    CreateOrderStatusHistory("new");
                    break;
                case 2:
                    ShowCourierFound();
                    CreateOrderStatusHistory("assigned");
                    break;
                case 3:
                    ShowInTransit();
                    CreateOrderStatusHistory("in_transit");
                    animationTimer.Stop();
                    payButton.Visible = true;
                    break;
            }
        }

        private void ShowSearchingCourier()
        {
            animationPanel.BackColor = Color.LightYellow;
            emojiLabel.Text = "🔍";
            statusLabel.Text = "Поиск курьера...\nПожалуйста, подождите";
            FindRandomCourier();
        }

        private void FindRandomCourier()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT courier_id, courier_name, vehicle FROM couriers WHERE courier_status = 'free'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            var freeCouriers = new List<(int id, string name, string vehicle)>();
                            while (reader.Read())
                            {
                                freeCouriers.Add((reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                            }
                            reader.Close();

                            if (freeCouriers.Count > 0)
                            {
                                Random rand = new Random();
                                var randomCourier = freeCouriers[rand.Next(freeCouriers.Count)];
                                assignedCourierId = randomCourier.id;
                                assignedCourierName = randomCourier.name;
                                assignedCourierVehicle = randomCourier.vehicle;
                                AssignCourierToOrder();
                            }
                            else
                            {
                                FindAnyCourier();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска курьера: {ex.Message}");
                FindAnyCourier();
            }
        }

        private void FindAnyCourier()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 courier_id, courier_name, vehicle FROM couriers";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                assignedCourierId = reader.GetInt32(0);
                                assignedCourierName = reader.GetString(1);
                                assignedCourierVehicle = reader.GetString(2);
                            }
                            reader.Close();
                        }
                    }
                    AssignCourierToOrder();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска любого курьера: {ex.Message}");
            }
        }

        private void AssignCourierToOrder()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    if (!CheckOrderExists(order_id))
                    {
                        MessageBox.Show($"Заказ №{order_id} не существует в базе данных");
                        return;
                    }

                    string updateOrder = @"UPDATE orders SET courier_id = @courier_id WHERE order_id = @order_id";
                    using (SqlCommand updateCmd = new SqlCommand(updateOrder, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@courier_id", assignedCourierId);
                        updateCmd.Parameters.AddWithValue("@order_id", order_id);
                        updateCmd.ExecuteNonQuery();
                    }

                    string updateCourier = @"UPDATE couriers SET courier_status = 'busy' WHERE courier_id = @courier_id";
                    using (SqlCommand updateCmd = new SqlCommand(updateCourier, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@courier_id", assignedCourierId);
                        updateCmd.ExecuteNonQuery();
                    }

                    string insertAction = @"INSERT INTO courier_actions (courier_id, order_id, courier_action, notes) 
                                          VALUES (@courier_id, @order_id, 'accept_order', 'Курьер принял заказ')";
                    using (SqlCommand actionCmd = new SqlCommand(insertAction, conn))
                    {
                        actionCmd.Parameters.AddWithValue("@courier_id", assignedCourierId);
                        actionCmd.Parameters.AddWithValue("@order_id", order_id);
                        actionCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка назначения курьера: {ex.Message}");
            }
        }

        private void ShowCourierFound()
        {
            animationPanel.BackColor = Color.LightGreen;
            emojiLabel.Text = "✅";
            statusLabel.Text = $"Курьер найден!\n{assignedCourierName} заберёт ваш заказ";
            RecordCourierAction("pickup_package", "Забрал посылку у магазина");
        }

        private void ShowInTransit()
        {
            animationPanel.BackColor = Color.LightBlue;
            emojiLabel.Text = "🚗";
            statusLabel.Text = $"Заказ в пути!\nКурьер уже везёт ваш заказ.\nТранспорт: {assignedCourierVehicle}";
            UpdateOrderStatus("in_transit");
            RecordCourierAction("start_delivery", "Начал доставку к клиенту");
        }

        private void UpdateOrderStatus(string status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE orders SET order_status = @status WHERE order_id = @order_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@order_id", order_id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления статуса: {ex.Message}");
            }
        }

        private void RecordCourierAction(string action, string notes)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    if (!CheckOrderExists(order_id))
                    {
                        MessageBox.Show($"Заказ №{order_id} не существует в базе данных");
                        return;
                    }

                    string query = @"INSERT INTO courier_actions (courier_id, order_id, courier_action, notes) 
                                   VALUES (@courier_id, @order_id, @action, @notes)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@courier_id", assignedCourierId);
                        cmd.Parameters.AddWithValue("@order_id", order_id);
                        cmd.Parameters.AddWithValue("@action", action);
                        cmd.Parameters.AddWithValue("@notes", notes);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи действия: {ex.Message}");
            }
        }

        private bool CheckOrderExists(int orderId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM orders WHERE order_id = @order_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@order_id", orderId);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проверки заказа: {ex.Message}");
                return false;
            }
        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            payButton.Enabled = false;
            payButton.Text = "ОБРАБОТКА...";
            payButton.BackColor = Color.Orange;

            Task.Run(async () =>
            {
                this.Invoke(new Action(() =>
                {
                    CreatePaymentAction("pending");
                }));

                await Task.Delay(2000);

                this.Invoke(new Action(() =>
                {
                    try
                    {
                        payButton.Text = "✅ ОПЛАЧЕНО";
                        payButton.BackColor = Color.Green;

                        CreateOrderStatusHistory("delivered");

                        //MessageBox.Show("Оплата принята в обработку! Заказ доставлен!", "Оплата",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CompletePayment();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка оплаты: {ex.Message}");
                        payButton.Enabled = true;
                        payButton.Text = "ОПЛАТИТЬ ЗАКАЗ";
                        payButton.BackColor = Color.ForestGreen;
                    }
                }));
            });
        }

        private void CompletePayment()
        {
            Task.Run(async () =>
            {
                await Task.Delay(3000);

                this.Invoke(new Action(() =>
                {
                    try
                    {
                        CreatePaymentAction("completed");

                        RecordCourierAction("complete_delivery", "Доставка завершена");

                        FreeCourier();

                        animationPanel.BackColor = Color.Honeydew;
                        emojiLabel.Text = "🎉";
                        statusLabel.Text = "Заказ доставлен!\nОплата завершена!\nСпасибо за заказ!";

                        //MessageBox.Show("✅ Заказ успешно доставлен и оплачен!", "Доставка завершена",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Task.Run(async () =>
                        {
                            await Task.Delay(2000);
                            this.Invoke(new Action(() =>
                            {
                                this.Close();
                                clientForm?.Show();
                            }));
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка завершения доставки: {ex.Message}");
                    }
                }));
            });
        }

        private void FreeCourier()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE couriers SET courier_status = 'free' WHERE courier_id = @courier_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@courier_id", assignedCourierId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка освобождения курьера: {ex.Message}");
            }
        }
        private void CreatePaymentAction(string status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"INSERT INTO payments (order_id, amount, method, payment_status, payment_date) 
                           VALUES (@order_id, @amount, @method, @status, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@order_id", order_id);
                        cmd.Parameters.AddWithValue("@amount", order_price);
                        cmd.Parameters.AddWithValue("@method", method);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания действия оплаты: {ex.Message}");
            }
        }

        private void CreateOrderStatusHistory(string status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    int? courierIdToUse = null;

                    if (status == "assigned" || status == "in_transit" || status == "delivered")
                    {
                        courierIdToUse = assignedCourierId;
                    }

                    string query = @"INSERT INTO order_status_history (order_id, courier_id, order_status, status_date) 
                           VALUES (@order_id, @courier_id, @status, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@order_id", order_id);
                        cmd.Parameters.AddWithValue("@courier_id", courierIdToUse ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.ExecuteNonQuery();
                    }

                    query = "UPDATE orders SET order_status = @status WHERE order_id = @order_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@order_id", order_id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания истории статуса: {ex.Message}");
            }
        }

        private void UserOrdersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientForm?.Show();
        }
    }
}