namespace Courier_delivery_service_PRJ
{
    partial class SignUpForm
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
            this.returnBackButton = new System.Windows.Forms.Button();
            this.ChooseLabel = new System.Windows.Forms.Label();
            this.ChooseComboBox = new System.Windows.Forms.ComboBox();
            this.SignUpButton = new System.Windows.Forms.Button();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.LoginNameLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.PhoneLabel = new System.Windows.Forms.Label();
            this.PhoneTextBox = new System.Windows.Forms.TextBox();
            this.EmailORvehicleLabel = new System.Windows.Forms.Label();
            this.EmailORvehicleTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // EntryLabel
            // 
            this.EntryLabel.AutoSize = true;
            this.EntryLabel.Font = new System.Drawing.Font("Rubik Black", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EntryLabel.Location = new System.Drawing.Point(61, 9);
            this.EntryLabel.Name = "EntryLabel";
            this.EntryLabel.Size = new System.Drawing.Size(690, 66);
            this.EntryLabel.TabIndex = 3;
            this.EntryLabel.Text = "Регистрация в системе";
            // 
            // returnBackButton
            // 
            this.returnBackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.returnBackButton.Location = new System.Drawing.Point(12, 412);
            this.returnBackButton.Name = "returnBackButton";
            this.returnBackButton.Size = new System.Drawing.Size(75, 37);
            this.returnBackButton.TabIndex = 11;
            this.returnBackButton.Text = "<";
            this.returnBackButton.UseVisualStyleBackColor = true;
            this.returnBackButton.Click += new System.EventHandler(this.returnBackButton_Click);
            // 
            // ChooseLabel
            // 
            this.ChooseLabel.Location = new System.Drawing.Point(139, 78);
            this.ChooseLabel.Name = "ChooseLabel";
            this.ChooseLabel.Size = new System.Drawing.Size(178, 33);
            this.ChooseLabel.TabIndex = 18;
            this.ChooseLabel.Text = "Выберите, кто вы:";
            // 
            // ChooseComboBox
            // 
            this.ChooseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseComboBox.FormattingEnabled = true;
            this.ChooseComboBox.Items.AddRange(new object[] {
            "Пользователь",
            "Курьер"});
            this.ChooseComboBox.Location = new System.Drawing.Point(323, 78);
            this.ChooseComboBox.Name = "ChooseComboBox";
            this.ChooseComboBox.Size = new System.Drawing.Size(175, 21);
            this.ChooseComboBox.TabIndex = 17;
            this.ChooseComboBox.TextChanged += new System.EventHandler(this.ChooseComboBox_TextChanged);
            // 
            // SignUpButton
            // 
            this.SignUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SignUpButton.Location = new System.Drawing.Point(323, 347);
            this.SignUpButton.Name = "SignUpButton";
            this.SignUpButton.Size = new System.Drawing.Size(175, 50);
            this.SignUpButton.TabIndex = 16;
            this.SignUpButton.Text = "ЗАРЕГИСТРИРОВАТЬСЯ";
            this.SignUpButton.UseVisualStyleBackColor = true;
            this.SignUpButton.Click += new System.EventHandler(this.SignUpButton_Click);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Location = new System.Drawing.Point(132, 283);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(178, 33);
            this.PasswordLabel.TabIndex = 15;
            this.PasswordLabel.Text = "Введите ваш пароль:";
            // 
            // LoginNameLabel
            // 
            this.LoginNameLabel.Location = new System.Drawing.Point(132, 125);
            this.LoginNameLabel.Name = "LoginNameLabel";
            this.LoginNameLabel.Size = new System.Drawing.Size(178, 33);
            this.LoginNameLabel.TabIndex = 14;
            this.LoginNameLabel.Text = "Введите ваше Имя и Фамилию:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(323, 279);
            this.PasswordTextBox.Multiline = true;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(175, 46);
            this.PasswordTextBox.TabIndex = 13;
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(323, 123);
            this.LoginTextBox.Multiline = true;
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(175, 46);
            this.LoginTextBox.TabIndex = 12;
            // 
            // PhoneLabel
            // 
            this.PhoneLabel.Location = new System.Drawing.Point(132, 177);
            this.PhoneLabel.Name = "PhoneLabel";
            this.PhoneLabel.Size = new System.Drawing.Size(178, 33);
            this.PhoneLabel.TabIndex = 20;
            this.PhoneLabel.Text = "Введите ваш номер телефона (+ в начале):";
            // 
            // PhoneTextBox
            // 
            this.PhoneTextBox.Location = new System.Drawing.Point(323, 175);
            this.PhoneTextBox.Multiline = true;
            this.PhoneTextBox.Name = "PhoneTextBox";
            this.PhoneTextBox.Size = new System.Drawing.Size(175, 46);
            this.PhoneTextBox.TabIndex = 19;
            // 
            // EmailORvehicleLabel
            // 
            this.EmailORvehicleLabel.Location = new System.Drawing.Point(132, 229);
            this.EmailORvehicleLabel.Name = "EmailORvehicleLabel";
            this.EmailORvehicleLabel.Size = new System.Drawing.Size(178, 33);
            this.EmailORvehicleLabel.TabIndex = 22;
            // 
            // EmailORvehicleTextBox
            // 
            this.EmailORvehicleTextBox.Enabled = false;
            this.EmailORvehicleTextBox.Location = new System.Drawing.Point(323, 227);
            this.EmailORvehicleTextBox.Multiline = true;
            this.EmailORvehicleTextBox.Name = "EmailORvehicleTextBox";
            this.EmailORvehicleTextBox.Size = new System.Drawing.Size(175, 46);
            this.EmailORvehicleTextBox.TabIndex = 21;
            // 
            // SignUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 461);
            this.Controls.Add(this.EmailORvehicleLabel);
            this.Controls.Add(this.EmailORvehicleTextBox);
            this.Controls.Add(this.PhoneLabel);
            this.Controls.Add(this.PhoneTextBox);
            this.Controls.Add(this.ChooseLabel);
            this.Controls.Add(this.ChooseComboBox);
            this.Controls.Add(this.SignUpButton);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginNameLabel);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.returnBackButton);
            this.Controls.Add(this.EntryLabel);
            this.MaximumSize = new System.Drawing.Size(820, 500);
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "SignUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignUpForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SignUpForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EntryLabel;
        private System.Windows.Forms.Button returnBackButton;
        private System.Windows.Forms.Label ChooseLabel;
        private System.Windows.Forms.ComboBox ChooseComboBox;
        private System.Windows.Forms.Button SignUpButton;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label LoginNameLabel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.Label PhoneLabel;
        private System.Windows.Forms.TextBox PhoneTextBox;
        private System.Windows.Forms.Label EmailORvehicleLabel;
        private System.Windows.Forms.TextBox EmailORvehicleTextBox;
    }
}