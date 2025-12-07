namespace Courier_delivery_service_PRJ
{
    partial class SendMoneyForm
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
            this.ChooseComboBox = new System.Windows.Forms.ComboBox();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.SendMoneyButton = new System.Windows.Forms.Button();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.QuantityLabel = new System.Windows.Forms.Label();
            this.ChooseLabel = new System.Windows.Forms.Label();
            this.QuantityTextBox = new System.Windows.Forms.TextBox();
            this.returnBackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChooseComboBox
            // 
            this.ChooseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseComboBox.FormattingEnabled = true;
            this.ChooseComboBox.Items.AddRange(new object[] {
            "Пользователь",
            "Курьер"});
            this.ChooseComboBox.Location = new System.Drawing.Point(211, 12);
            this.ChooseComboBox.Name = "ChooseComboBox";
            this.ChooseComboBox.Size = new System.Drawing.Size(175, 21);
            this.ChooseComboBox.TabIndex = 18;
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(211, 39);
            this.LoginTextBox.Multiline = true;
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(175, 46);
            this.LoginTextBox.TabIndex = 19;
            // 
            // SendMoneyButton
            // 
            this.SendMoneyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendMoneyButton.Location = new System.Drawing.Point(211, 143);
            this.SendMoneyButton.Name = "SendMoneyButton";
            this.SendMoneyButton.Size = new System.Drawing.Size(175, 50);
            this.SendMoneyButton.TabIndex = 22;
            this.SendMoneyButton.Text = "Отправить";
            this.SendMoneyButton.UseVisualStyleBackColor = true;
            this.SendMoneyButton.Click += new System.EventHandler(this.SendMoneyButton_Click);
            // 
            // LoginLabel
            // 
            this.LoginLabel.Location = new System.Drawing.Point(12, 39);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(130, 46);
            this.LoginLabel.TabIndex = 25;
            this.LoginLabel.Text = "Укажите (имя, фамилия) или телефон этого человека (логин):";
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.Location = new System.Drawing.Point(12, 91);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(90, 33);
            this.QuantityLabel.TabIndex = 24;
            this.QuantityLabel.Text = "Введите сумму:";
            // 
            // ChooseLabel
            // 
            this.ChooseLabel.Location = new System.Drawing.Point(11, 12);
            this.ChooseLabel.Name = "ChooseLabel";
            this.ChooseLabel.Size = new System.Drawing.Size(194, 21);
            this.ChooseLabel.TabIndex = 23;
            this.ChooseLabel.Text = "Кому вы хотите отправить деньги:";
            // 
            // QuantityTextBox
            // 
            this.QuantityTextBox.Location = new System.Drawing.Point(211, 91);
            this.QuantityTextBox.Multiline = true;
            this.QuantityTextBox.Name = "QuantityTextBox";
            this.QuantityTextBox.Size = new System.Drawing.Size(175, 46);
            this.QuantityTextBox.TabIndex = 27;
            // 
            // returnBackButton
            // 
            this.returnBackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.returnBackButton.Location = new System.Drawing.Point(15, 156);
            this.returnBackButton.Name = "returnBackButton";
            this.returnBackButton.Size = new System.Drawing.Size(75, 37);
            this.returnBackButton.TabIndex = 28;
            this.returnBackButton.Text = "<";
            this.returnBackButton.UseVisualStyleBackColor = true;
            this.returnBackButton.Click += new System.EventHandler(this.returnBackButton_Click);
            // 
            // SendMoneyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 203);
            this.Controls.Add(this.returnBackButton);
            this.Controls.Add(this.QuantityTextBox);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.QuantityLabel);
            this.Controls.Add(this.ChooseLabel);
            this.Controls.Add(this.SendMoneyButton);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.ChooseComboBox);
            this.Name = "SendMoneyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SendMoneyForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SendMoneyForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ChooseComboBox;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.Button SendMoneyButton;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.Label ChooseLabel;
        private System.Windows.Forms.TextBox QuantityTextBox;
        private System.Windows.Forms.Button returnBackButton;
    }
}