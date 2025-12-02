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
    public partial class ClientForm : Form
    {
        public Form1 form1 {  get; set; }
        //public Ordering ordering { get; set; }
        string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        public int client_id { get; set; }

        public ClientForm()
        {
            InitializeComponent();
            LoadCategoriesToComboBox();
            LoadProducts();
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if(button.Tag == null)
            {
                MessageBox.Show("Ошибка: Tag кнопки пустой!");
                return;
            }
            
            int productId = (int)button.Tag;
            //MessageBox.Show($"Передаем productId: {productId}");

            CreateOrder(productId);
        }

        private void CreateOrder(int productId)
        {
            Ordering orderingForm = new Ordering();
            orderingForm.form1 = form1;
            orderingForm.clientForm = this;
            orderingForm.client_id = client_id;
            orderingForm.productId = productId;
            orderingForm.LoadProductInfo();
            orderingForm.Show();
            //ordering = orderingForm;
            this.Hide();
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            product_id,
                            product_name,
                            description,
                            price,
                            category,
                            image_url
                        FROM products 
                        WHERE is_available = 1
                        ORDER BY category, price";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable productsTable = new DataTable();
                            adapter.Fill(productsTable);
                            DisplayProducts(productsTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки продуктов: {ex.Message}");
            }
        }

        private void DisplayProducts(DataTable productsTable)
        {
            flowLayoutPanel.Controls.Clear();

            foreach (DataRow row in productsTable.Rows)
            {
                Panel productCard = CreateProductCard(row);
                flowLayoutPanel.Controls.Add(productCard);
            }
        }

        private Panel CreateProductCard(DataRow product)
        {
            Panel card = new Panel
            {
                Size = new Size(280, 200),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(10),
                Tag = product["product_id"]
            };

            Label titleLabel = new Label
            {
                Text = product["product_name"].ToString(),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(10, 10),
                Size = new Size(260, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label categoryLabel = new Label
            {
                Text = product["category"].ToString().ToUpper(),
                Font = new Font("Arial", 8, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetCategoryColor(product["category"].ToString()),
                Location = new Point(10, 40),
                Size = new Size(80, 18),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter
            };

            TextBox descriptionBox = new TextBox
            {
                Text = product["description"].ToString(),
                Location = new Point(10, 65),
                Size = new Size(260, 60),
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.White,
                Font = new Font("Arial", 9),
                ForeColor = Color.DarkGray
            };

            Label priceLabel = new Label
            {
                Text = $"{Convert.ToDecimal(product["price"]):C}",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Green,
                Location = new Point(10, 135),
                Size = new Size(120, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Button orderButton = new Button
            {
                Text = "ЗАКАЗАТЬ",
                Location = new Point(140, 135),
                Size = new Size(130, 35),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Tag = product["product_id"]
            };
            orderButton.FlatAppearance.BorderSize = 0;
            orderButton.Click += OrderButton_Click;

            orderButton.MouseEnter += (s, e) => { orderButton.BackColor = Color.DodgerBlue; };
            orderButton.MouseLeave += (s, e) => { orderButton.BackColor = Color.SteelBlue; };

            card.MouseEnter += (s, e) => { card.BackColor = Color.LightCyan; descriptionBox.BackColor = Color.LightCyan; };
            card.MouseLeave += (s, e) => { card.BackColor = Color.White; descriptionBox.BackColor = Color.White; };
            descriptionBox.MouseEnter += (s, e) => { card.BackColor = Color.LightCyan; descriptionBox.BackColor = Color.LightCyan; };
            descriptionBox.MouseLeave += (s, e) => { card.BackColor = Color.White; descriptionBox.BackColor = Color.White; };

            card.Controls.AddRange(new Control[] {
                titleLabel, categoryLabel, descriptionBox, priceLabel, orderButton
            });

            return card;
        }

        private Color GetCategoryColor(string category)
        {
            if (category.ToLower() == "одежда")
                return Color.Red;
            else if (category.ToLower() == "электроника")
                return Color.DarkBlue;
            else if (category.ToLower() == "еда")
                return Color.Orange;
            else if (category.ToLower() == "мебель")
                return Color.Green;
            else
                return Color.Gray;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterProducts(searchTextBox.Text);
        }

        private void FilterProducts(string searchText)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            product_id,
                            product_name,
                            description,
                            price,
                            category,
                            image_url
                        FROM products 
                        WHERE is_available = 1 
                          AND (product_name LIKE @search OR description LIKE @search OR category LIKE @search)";

                    if (!string.IsNullOrEmpty(categoryComboBox.Text) && categoryComboBox.Text != "Все категории")
                    {
                        query += " AND category = @category";
                    }

                    query += " ORDER BY category, product_name";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@search", $"%{searchText}%");
                        if (!string.IsNullOrEmpty(categoryComboBox.Text) && categoryComboBox.Text != "Все категории")
                        {
                            command.Parameters.AddWithValue("@category", categoryComboBox.Text);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable productsTable = new DataTable();
                            adapter.Fill(productsTable);
                            DisplayProducts(productsTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedIndex == 0)
            {
                LoadProducts();
            }
            else
            {
                FilterByCategory(categoryComboBox.SelectedItem.ToString());
            }
        }

        private void FilterByCategory(string category)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            product_id,
                            product_name,
                            description,
                            price,
                            category,
                            image_url
                        FROM products 
                        WHERE is_available = 1 AND category = @category
                        ORDER BY price";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@category", category);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable productsTable = new DataTable();
                            adapter.Fill(productsTable);
                            DisplayProducts(productsTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}");
            }
        }

        private void LoadCategoriesToComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    string query = @"
                SELECT DISTINCT category 
                FROM products 
                WHERE is_available = 1
                ORDER BY category";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            categoryComboBox.Items.Clear();

                            categoryComboBox.Items.Add("Все категории");

                            while (reader.Read())
                            {
                                string category = reader["category"].ToString();
                                categoryComboBox.Items.Add(category);
                            }

                            categoryComboBox.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}");
            }
        }

        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
