using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Courier_delivery_service_PRJ
{
    public partial class DeliveryProcessForm : Form
    {
        private string connStr = @"Data Source=DESKTOP-O03Q1EM;Initial Catalog=Courier_delivery_service;Integrated Security=True";

        public int OrderId { get; set; }
        public int CourierId { get; set; }
        public CourierForm courierForm { get; set; }

        private int currentStep = 1;
        private Label statusLabel;
        private Label emojiLabel;
        private Panel mainPanel;
        private Button currentActionButton;

        public DeliveryProcessForm(int orderId, int courierId, CourierForm form)
        {
            OrderId = orderId;
            CourierId = courierId;
            courierForm = form;

            InitializeComponents();
            currentStep = 1;
        }

        private void InitializeComponents()
        {
            this.Size = new Size(520, 440);
            this.Text = $"Процесс доставки - Заказ №{OrderId}";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            mainPanel = new Panel
            {
                Size = new Size(460, 350),
                Location = new Point(20, 20),
                BackColor = Color.Lavender,
                BorderStyle = BorderStyle.FixedSingle
            };

            emojiLabel = new Label
            {
                Size = new Size(80, 80),
                Location = new Point(190, 30),
                Font = new Font("Arial", 36),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "✅"
            };

            statusLabel = new Label
            {
                Size = new Size(420, 60),
                Location = new Point(20, 120),
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkBlue,
                Text = "Заказ принят! Начинаем доставку..."
            };

            currentActionButton = new Button
            {
                Size = new Size(200, 40),
                Location = new Point(130, 200),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "🚚 ЗАБРАТЬ ЗАКАЗ",
                Visible = true
            };
            currentActionButton.FlatAppearance.BorderSize = 0;
            currentActionButton.Click += CurrentActionButton_Click;

            mainPanel.Controls.AddRange(new Control[] { emojiLabel, statusLabel, currentActionButton });
            this.Controls.Add(mainPanel);
        }

        private async void CurrentActionButton_Click(object sender, EventArgs e)
        {
            currentActionButton.Enabled = false;
            currentActionButton.Visible = false;

            switch (currentStep)
            {
                case 1:
                    statusLabel.Text = "Едем на пункт выдачи...";
                    emojiLabel.Text = "🚗";
                    await Task.Delay(3000);

                    await ShowPickupStep();
                    await Task.Delay(1000);
                    ShowActionButton("🚀 НАЧАТЬ ДОСТАВКУ", Color.DodgerBlue);
                    currentStep = 2;
                    break;

                case 2:
                    statusLabel.Text = "Подготавливаем заказ...";
                    emojiLabel.Text = "📦";
                    await Task.Delay(2000);

                    await ShowStartDeliveryStep();
                    statusLabel.Text = "Везём заказ клиенту...";
                    emojiLabel.Text = "🚚";
                    await Task.Delay(4000);

                    await Task.Delay(1000);
                    ShowActionButton("✅ ЗАВЕРШИТЬ ДОСТАВКУ", Color.Green);
                    currentStep = 3;
                    break;

                case 3:
                    statusLabel.Text = "Завершаем доставку...";
                    emojiLabel.Text = "⏳";
                    await Task.Delay(2000);

                    await ShowCompleteStep();
                    await Task.Delay(1000);
                    statusLabel.Text = "Доставка завершена!";
                    emojiLabel.Text = "🎉";
                    await CreatePayment("completed");
                    await Task.Delay(2000);

                    CloseAndReturn();
                    break;
            }
        }

        private void ShowActionButton(string text, Color color)
        {
            currentActionButton.Text = text;
            currentActionButton.BackColor = color;
            currentActionButton.Enabled = true;
            currentActionButton.Visible = true;
        }

        private async Task ShowPickupStep()
        {
            emojiLabel.Text = "📦";
            statusLabel.Text = "Заказ успешно забран!";
            mainPanel.BackColor = Color.LightGoldenrodYellow;

            //await UpdateOrderStatus("picked_up");
            await AddCourierAction("pickup_package", "Забрал заказ с пункта выдачи");
            //await CreateOrderStatusHistory("picked_up");
        }

        private async Task ShowStartDeliveryStep()
        {
            emojiLabel.Text = "🚀";
            statusLabel.Text = "Доставка начата!";
            mainPanel.BackColor = Color.LightSkyBlue;

            await UpdateOrderStatus("in_transit");
            await AddCourierAction("start_delivery", "Начал доставку к клиенту");
            await CreateOrderStatusHistory("in_transit");
            //await CreatePayment("waiting");
        }

        private async Task ShowCompleteStep()
        {
            emojiLabel.Text = "✅";
            statusLabel.Text = "Доставка завершена!";
            mainPanel.BackColor = Color.LightGreen;

            await CreatePayment("pending");
            await UpdateOrderStatus("delivered");
            await AddCourierAction("complete_delivery", "Доставка успешно завершена");
            await CreateOrderStatusHistory("delivered");
            await UpdateCourierStatus("free");
        }

        private async Task UpdateOrderStatus(string status)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                string query = "UPDATE orders SET order_status = @status WHERE order_id = @order_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@order_id", OrderId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        private async Task UpdateCourierStatus(string status)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                string query = "UPDATE couriers SET courier_status = @status WHERE courier_id = @courier_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@courier_id", CourierId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task AddCourierAction(string action, string notes)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                string query = @"INSERT INTO courier_actions (courier_id, order_id, courier_action, notes)
                               VALUES (@courier_id, @order_id, @action, @notes)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@courier_id", CourierId);
                    cmd.Parameters.AddWithValue("@order_id", OrderId);
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@notes", notes);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task CreateOrderStatusHistory(string status)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                string query = @"INSERT INTO order_status_history (order_id, courier_id, order_status)
                               VALUES (@order_id, @courier_id, @status)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", OrderId);
                    cmd.Parameters.AddWithValue("@courier_id", CourierId);
                    cmd.Parameters.AddWithValue("@status", status);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task CreatePayment(string status)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();

                string getPriceQuery = "SELECT price FROM orders WHERE order_id = @order_id";
                string getMethodQuery = "SELECT method FROM payments WHERE order_id = @order_id";
                decimal amount = 0;
                string method = "systemniy";
                using (SqlCommand cmd = new SqlCommand(getPriceQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", OrderId);
                    amount = Convert.ToDecimal(await cmd.ExecuteScalarAsync());
                }
                using (SqlCommand cmd = new SqlCommand(getMethodQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", OrderId);
                    method = Convert.ToString(await cmd.ExecuteScalarAsync());
                }

                string query = @"INSERT INTO payments (order_id, amount, method, payment_status, payment_date)
                               VALUES (@order_id, @amount, @method, @status, GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", OrderId);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@method", method);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private void CloseAndReturn()
        {
            courierForm?.RefreshOrders();
            courierForm?.Show();
            this.Close();
        }

        private void DeliveryProcessForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            courierForm?.Show();
        }
    }
}