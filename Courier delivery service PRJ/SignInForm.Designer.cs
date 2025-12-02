namespace Courier_delivery_service_PRJ
{
    partial class SignInForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EntryLabel = new System.Windows.Forms.Label();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.SignInButton = new System.Windows.Forms.Button();
            this.ChooseComboBox = new System.Windows.Forms.ComboBox();
            this.ChooseLabel = new System.Windows.Forms.Label();
            this.returnBackButton = new System.Windows.Forms.Button();
            this.adminWarningLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EntryLabel
            // 
            this.EntryLabel.AutoSize = true;
            this.EntryLabel.Font = new System.Drawing.Font("Rubik Black", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EntryLabel.Location = new System.Drawing.Point(154, 9);
            this.EntryLabel.Name = "EntryLabel";
            this.EntryLabel.Size = new System.Drawing.Size(463, 66);
            this.EntryLabel.TabIndex = 2;
            this.EntryLabel.Text = "Вход в систему";
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(300, 140);
            this.LoginTextBox.Multiline = true;
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(175, 46);
            this.LoginTextBox.TabIndex = 3;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(300, 210);
            this.PasswordTextBox.Multiline = true;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(175, 46);
            this.PasswordTextBox.TabIndex = 4;
            // 
            // LoginLabel
            // 
            this.LoginLabel.Location = new System.Drawing.Point(109, 142);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(178, 33);
            this.LoginLabel.TabIndex = 5;
            this.LoginLabel.Text = "Введите ваш логин (Имя и Фамилия или номер телефона):";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Location = new System.Drawing.Point(109, 214);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(178, 33);
            this.PasswordLabel.TabIndex = 6;
            this.PasswordLabel.Text = "Введите ваш пароль:";
            // 
            // SignInButton
            // 
            this.SignInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SignInButton.Location = new System.Drawing.Point(310, 279);
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.Size = new System.Drawing.Size(140, 50);
            this.SignInButton.TabIndex = 7;
            this.SignInButton.Text = "ВОЙТИ";
            this.SignInButton.UseVisualStyleBackColor = true;
            this.SignInButton.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // ChooseComboBox
            // 
            this.ChooseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseComboBox.FormattingEnabled = true;
            this.ChooseComboBox.Items.AddRange(new object[] {
            "Пользователь",
            "Курьер",
            "Администратор"});
            this.ChooseComboBox.Location = new System.Drawing.Point(300, 95);
            this.ChooseComboBox.Name = "ChooseComboBox";
            this.ChooseComboBox.Size = new System.Drawing.Size(175, 21);
            this.ChooseComboBox.TabIndex = 8;
            this.ChooseComboBox.SelectedIndexChanged += new System.EventHandler(this.ChooseComboBox_SelectedIndexChanged);
            // 
            // ChooseLabel
            // 
            this.ChooseLabel.Location = new System.Drawing.Point(116, 95);
            this.ChooseLabel.Name = "ChooseLabel";
            this.ChooseLabel.Size = new System.Drawing.Size(178, 33);
            this.ChooseLabel.TabIndex = 9;
            this.ChooseLabel.Text = "Выберите, кто вы:";
            // 
            // returnBackButton
            // 
            this.returnBackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.returnBackButton.Location = new System.Drawing.Point(12, 412);
            this.returnBackButton.Name = "returnBackButton";
            this.returnBackButton.Size = new System.Drawing.Size(75, 37);
            this.returnBackButton.TabIndex = 10;
            this.returnBackButton.Text = "<";
            this.returnBackButton.UseVisualStyleBackColor = true;
            this.returnBackButton.Click += new System.EventHandler(this.returnBackButton_Click);
            // 
            // adminWarningLabel
            // 
            this.adminWarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.adminWarningLabel.Location = new System.Drawing.Point(481, 95);
            this.adminWarningLabel.Name = "adminWarningLabel";
            this.adminWarningLabel.Size = new System.Drawing.Size(311, 35);
            this.adminWarningLabel.TabIndex = 11;
            this.adminWarningLabel.Text = "*Чтобы войти в аккаунт администратора, обратитесь к владельцу";
            this.adminWarningLabel.Visible = false;
            // 
            // SignInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 461);
            this.Controls.Add(this.adminWarningLabel);
            this.Controls.Add(this.returnBackButton);
            this.Controls.Add(this.ChooseLabel);
            this.Controls.Add(this.ChooseComboBox);
            this.Controls.Add(this.SignInButton);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.EntryLabel);
            this.MaximumSize = new System.Drawing.Size(820, 500);
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "SignInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignInForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SignInForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EntryLabel;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button SignInButton;
        private System.Windows.Forms.ComboBox ChooseComboBox;
        private System.Windows.Forms.Label ChooseLabel;
        private System.Windows.Forms.Button returnBackButton;
        private System.Windows.Forms.Label adminWarningLabel;
    }
}