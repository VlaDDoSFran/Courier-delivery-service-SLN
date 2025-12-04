using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Courier_delivery_service_PRJ
{
    public partial class AdminForm : Form
    {
        public Form1 form1 { get; set; }
        public int admin_id { get; set; }
        private string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        private DataTable currentTable;
        private string currentTableName;

        public AdminForm()
        {
            InitializeComponent();
            LoadTableNames();
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
                        AND TABLE_NAME <> 'sysdiagrams' AND TABLE_NAME <> 'admin_auth'
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
            }
        }

        private void tableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tableComboBox.SelectedItem != null)
            {
                string tableName = tableComboBox.SelectedItem.ToString();
                LoadTableData(tableName);

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
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentTableName))
            {
                LoadTableData(currentTableName);
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

                        var identityColumns = new List<string>();
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

                string columnTypes = string.Join("\n", columns.Select(c => $"{c.ColumnName} ({GetTypeDescription(c.DataType)})"));

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
                {
                    if (value == "1" || value.ToLower() == "true" || value.ToLower() == "да")
                        return true;
                    else if (value == "0" || value.ToLower() == "false" || value.ToLower() == "нет")
                        return false;
                    return bool.Parse(value);
                }
                else
                    return value;
            }
            catch
            {
                return value;
            }
        }

        private string GetTypeDescription(Type type)
        {
            if (type == typeof(int) || type == typeof(Int32))
                return "целое число";
            else if (type == typeof(decimal) || type == typeof(Decimal))
                return "число с запятой";
            else if (type == typeof(DateTime))
                return "дата (гггг-мм-дд)";
            else if (type == typeof(bool) || type == typeof(Boolean))
                return "да/нет (1/0)";
            else
                return "текст";
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

                    string primaryKeyColumnName = GetPrimaryKeyColumn(currentTableName);
                    object primaryKeyValue = row[primaryKeyColumnName];

                    var columns = currentTable.Columns.Cast<DataColumn>()
                        .Where(c => !string.Equals(c.ColumnName, primaryKeyColumnName, StringComparison.OrdinalIgnoreCase))
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
                        currentValues += $"{column.ColumnName} ({GetTypeDescription(column.DataType)}): {value}\n";
                    }

                    string columnsText = string.Join(", ", columns.Select(c => c.ColumnName));
                    string input = Microsoft.VisualBasic.Interaction.InputBox(
                        $"Текущие значения:\n{currentValues}\n\nВведите новые значения через запятую для колонок:\n{columnsText}",
                        $"Обновление записи в {currentTableName}", "");

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

                    string query = $"UPDATE {currentTableName} SET {setClause} WHERE {primaryKeyColumnName} = @pk";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@pk", primaryKeyValue);

                        for (int i = 0; i < columns.Length; i++)
                        {
                            string paramName = "@" + columns[i].ColumnName;
                            object value = string.IsNullOrEmpty(values[i]) ? DBNull.Value : ConvertValue(values[i], columns[i].DataType);
                            cmd.Parameters.AddWithValue(paramName, value);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

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

                    string primaryKeyColumnName = GetPrimaryKeyColumn(currentTableName);
                    int deletedCount = 0;

                    foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                    {
                        DataRowView rowView = (DataRowView)selectedRow.DataBoundItem;
                        DataRow row = rowView.Row;
                        object primaryKeyValue = row[primaryKeyColumnName];

                        string query = $"DELETE FROM {currentTableName} WHERE {primaryKeyColumnName} = @pk";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@pk", primaryKeyValue);
                            deletedCount += cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Удалено {deletedCount} записей", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTableData(currentTableName);
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Нельзя удалить запись, так как на нее ссылаются другие таблицы",
                    "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1?.Close();
        }
    }
}