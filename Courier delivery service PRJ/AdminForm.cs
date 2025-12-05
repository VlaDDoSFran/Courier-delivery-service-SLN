using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Courier_delivery_service_PRJ
{
    public partial class AdminForm : Form
    {
        public Form1 form1 { get; set; }
        public int admin_id { get; set; }
        private string admin_name { get; set; }
        private string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        private DataTable currentTable;
        private string currentTableName;

        public AdminForm(Form1 form, int adminId)
        {
            form1 = form;
            admin_id = adminId;
            InitializeComponent();

            LoadAdminInfo();
            LoadTableNames();

            LogAdminAction("SELECT", "INIT", "Администратор открыл форму управления");
        }

        public AdminForm()
        {
            InitializeComponent();

            LoadAdminInfo();
            LoadTableNames();

            LogAdminAction("SELECT", "INIT", "Администратор открыл форму управления");
        }

        private void LoadAdminInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT admin_name FROM admins WHERE admin_id = @admin_id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@admin_id", admin_id);
                        admin_name = cmd.ExecuteScalar()?.ToString() ?? "Unknown";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки информации об администраторе: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                admin_name = "Unknown";
            }
        }

        private void LogAdminAction(string action, string data = "", string details = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO admin_actions 
                        (admin_id, admin_name, selected_table, admin_action, action_data, action_date)
                        VALUES (@adminId, @admin_name, @selected_table, @admin_action, @action_data, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@adminId", admin_id);
                        cmd.Parameters.AddWithValue("@admin_name", admin_name);
                        cmd.Parameters.AddWithValue("@selected_table",
                            string.IsNullOrEmpty(currentTableName) ? "N/A" : currentTableName);
                        cmd.Parameters.AddWithValue("@admin_action", action);
                        cmd.Parameters.AddWithValue("@action_data",
                            string.IsNullOrEmpty(data) ? details : $"{details} | Данные: {data}");

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка логирования: {ex.Message}");
            }
        }

        private void LoadTableNames()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"
                        SELECT TABLE_NAME 
                        FROM INFORMATION_SCHEMA.TABLES 
                        WHERE TABLE_TYPE = 'BASE TABLE'
                        AND TABLE_NAME <> 'sysdiagrams'
                        AND TABLE_NAME <> 'admin_auth'
                        ORDER BY TABLE_NAME";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableComboBox.Items.Add(reader["TABLE_NAME"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки таблиц: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAdminAction("ERROR", "LoadTableNames", $"Ошибка: {ex.Message}");
            }
        }

        private void tableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tableComboBox.SelectedItem != null)
            {
                string tableName = tableComboBox.SelectedItem.ToString();
                LoadTableData(tableName);

                LogAdminAction("SELECT", tableName, "Выбор таблицы для просмотра");

                if (tableName == "admins" || tableName == "admin_actions" || tableName == "admin_auth")
                {
                    insertButton.Enabled = false;
                    updateButton.Enabled = false;
                    deleteButton.Enabled = false;

                    insertButton.Text = "Добавить (заблокировано)";
                    updateButton.Text = "Изменить (заблокировано)";
                    deleteButton.Text = "Удалить (заблокировано)";

                    insertButton.BackColor = Color.Gray;
                    updateButton.BackColor = Color.Gray;
                    deleteButton.BackColor = Color.Gray;

                    statusLabel.Text = $"Таблица: {tableName} | Записей: {currentTable.Rows.Count} | РЕДАКТИРОВАНИЕ ЗАПРЕЩЕНО";
                }
                else
                {
                    insertButton.Enabled = true;
                    updateButton.Enabled = true;
                    deleteButton.Enabled = true;

                    insertButton.Text = "Добавить";
                    updateButton.Text = "Изменить";
                    deleteButton.Text = "Удалить";

                    insertButton.BackColor = Color.LightGreen;
                    updateButton.BackColor = Color.LightYellow;
                    deleteButton.BackColor = Color.LightCoral;
                }
            }
        }

        private void LoadTableData(string tableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = $"SELECT * FROM {tableName}";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        currentTable = new DataTable();
                        adapter.Fill(currentTable);

                        dataGridView1.DataSource = currentTable;
                        currentTableName = tableName;

                        statusLabel.Text = $"Таблица: {tableName} | Записей: {currentTable.Rows.Count}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAdminAction("ERROR", tableName, $"Ошибка загрузки: {ex.Message}");
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentTableName))
            {
                LoadTableData(currentTableName);
                LogAdminAction("SELECT", currentTableName, "Обновление данных таблицы");
            }
        }

        private DataColumn[] GetNonIdentityColumns(string tableName, DataTable table)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string query = @"
                        SELECT COLUMN_NAME
                        FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE TABLE_NAME = @TableName 
                        AND COLUMNPROPERTY(
                            OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), 
                            COLUMN_NAME, 
                            'IsIdentity'
                        ) = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableName", tableName);

                        var identityColumns = new System.Collections.Generic.List<string>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                identityColumns.Add(reader["COLUMN_NAME"].ToString());
                            }
                        }

                        return table.Columns.Cast<DataColumn>()
                            .Where(c => !identityColumns.Contains(c.ColumnName, StringComparer.OrdinalIgnoreCase))
                            .ToArray();
                    }
                }
            }
            catch (Exception)
            {
                return table.Columns.Cast<DataColumn>()
                    .Where((c, index) => index > 0)
                    .ToArray();
            }
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            if (currentTable == null)
            {
                MessageBox.Show("Сначала выберите таблицу", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var columns = GetNonIdentityColumns(currentTableName, currentTable);

                if (columns.Length == 0)
                {
                    MessageBox.Show("Нет доступных полей для вставки", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string columnsText = string.Join(", ", columns.Select(c => c.ColumnName));
                string columnTypes = string.Join("\n", columns.Select(c => $"{c.ColumnName} ({c.DataType.Name})"));

                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Введите значения через запятую для колонок:\n\n{columnTypes}\n\nПример: значение1, значение2, ...",
                    $"Добавление записи в {currentTableName}", "");

                if (string.IsNullOrEmpty(input))
                    return;

                var values = input.Split(',').Select(v => v.Trim()).ToArray();

                if (values.Length != columns.Length)
                {
                    MessageBox.Show($"Неверное количество значений. Ожидается {columns.Length}, получено {values.Length}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string columnNames = string.Join(", ", columns.Select(c => c.ColumnName));
                    string paramNames = string.Join(", ", columns.Select(c => "@" + c.ColumnName));

                    string query = $"INSERT INTO {currentTableName} ({columnNames}) VALUES ({paramNames})";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        for (int i = 0; i < columns.Length; i++)
                        {
                            string paramName = "@" + columns[i].ColumnName;
                            object value = string.IsNullOrEmpty(values[i]) ? DBNull.Value : ConvertValue(values[i], columns[i].DataType);
                            cmd.Parameters.AddWithValue(paramName, value);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        string insertedData = $"Добавленные значения: {input}";
                        LogAdminAction("INSERT", currentTableName, insertedData);

                        MessageBox.Show($"Добавлено {rowsAffected} записей", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadTableData(currentTableName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAdminAction("ERROR", currentTableName, $"Ошибка INSERT: {ex.Message}");
            }
        }

        private object ConvertValue(string value, Type dataType)
        {
            if (string.IsNullOrEmpty(value))
                return DBNull.Value;

            try
            {
                if (dataType == typeof(int) || dataType == typeof(Int32))
                    return int.Parse(value);
                else if (dataType == typeof(decimal) || dataType == typeof(Decimal))
                    return decimal.Parse(value);
                else if (dataType == typeof(DateTime))
                    return DateTime.Parse(value);
                else if (dataType == typeof(bool) || dataType == typeof(Boolean))
                    return bool.Parse(value);
                else
                    return value;
            }
            catch
            {
                return value;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (currentTable == null || dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Сначала выберите таблицу и запись для обновления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    DataRowView rowView = (DataRowView)selectedRow.DataBoundItem;
                    DataRow row = rowView.Row;

                    string primaryKeyColumn = GetPrimaryKeyColumn(currentTableName);
                    object primaryKeyValue = row[primaryKeyColumn];

                    var columns = currentTable.Columns.Cast<DataColumn>()
                        .Where(c => !string.Equals(c.ColumnName, primaryKeyColumn, StringComparison.OrdinalIgnoreCase))
                        .ToArray();

                    if (columns.Length == 0)
                    {
                        MessageBox.Show("Нет доступных полей для обновления", "Внимание",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string currentValues = "";
                    foreach (var column in columns)
                    {
                        object value = row[column.ColumnName];
                        currentValues += $"{column.ColumnName}: {value}\n";
                    }

                    string columnsText = string.Join(", ", columns.Select(c => c.ColumnName));
                    string input = Microsoft.VisualBasic.Interaction.InputBox(
                        $"Текущие значения:\n{currentValues}\nВведите новые значения через запятую для колонок:\n{columnsText}",
                        "Обновление записи", "");

                    if (string.IsNullOrEmpty(input))
                        return;

                    var values = input.Split(',').Select(v => v.Trim()).ToArray();

                    if (values.Length != columns.Length)
                    {
                        MessageBox.Show($"Неверное количество значений. Ожидается {columns.Length}, получено {values.Length}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string setClause = string.Join(", ",
                        columns.Select(c => $"{c.ColumnName} = @{c.ColumnName}"));

                    string query = $"UPDATE {currentTableName} SET {setClause} WHERE {primaryKeyColumn} = @pk";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@pk", primaryKeyValue);

                        for (int i = 0; i < columns.Length; i++)
                        {
                            cmd.Parameters.AddWithValue("@" + columns[i].ColumnName,
                                string.IsNullOrEmpty(values[i]) ? DBNull.Value : (object)values[i]);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        string updateData = $"ID: {primaryKeyValue}, Новые значения: {input}";
                        LogAdminAction("UPDATE", currentTableName, updateData);

                        MessageBox.Show($"Обновлено {rowsAffected} записей", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadTableData(currentTableName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAdminAction("ERROR", currentTableName, $"Ошибка UPDATE: {ex.Message}");
            }
        }

        private string GetPrimaryKeyColumn(string tableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string query = @"
                        SELECT COLUMN_NAME
                        FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                        WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + CONSTRAINT_NAME), 'IsPrimaryKey') = 1
                        AND TABLE_NAME = @TableName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableName", tableName);

                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? currentTable.Columns[0].ColumnName;
                    }
                }
            }
            catch (Exception)
            {
                return currentTable.Columns[0].ColumnName;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (currentTable == null || dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Сначала выберите таблицу и записи для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить {dataGridView1.SelectedRows.Count} выбранных записей?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string primaryKeyColumn = GetPrimaryKeyColumn(currentTableName);
                    int deletedCount = 0;
                    string deletedIds = "";

                    foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                    {
                        DataRowView rowView = (DataRowView)selectedRow.DataBoundItem;
                        DataRow row = rowView.Row;
                        object primaryKeyValue = row[primaryKeyColumn];

                        string query = $"DELETE FROM {currentTableName} WHERE {primaryKeyColumn} = @pk";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@pk", primaryKeyValue);
                            deletedCount += cmd.ExecuteNonQuery();

                            if (!string.IsNullOrEmpty(deletedIds))
                                deletedIds += ", ";
                            deletedIds += primaryKeyValue.ToString();
                        }
                    }

                    string deleteData = $"Удалено ID: {deletedIds}";
                    LogAdminAction("DELETE", currentTableName, deleteData);

                    MessageBox.Show($"Удалено {deletedCount} записей", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTableData(currentTableName);
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Нельзя удалить запись, так как на нее ссылаются другие таблицы",
                    "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAdminAction("ERROR", currentTableName, $"Ошибка DELETE (547): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogAdminAction("ERROR", currentTableName, $"Ошибка DELETE: {ex.Message}");
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            LogAdminAction("LOGOUT", "EXIT", "Выход из системы администратора");
            this.Close();
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1?.Show();
        }
    }
}