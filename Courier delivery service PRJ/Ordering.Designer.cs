namespace Courier_delivery_service_PRJ
{
    partial class Ordering
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
            this.returnBackButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.paymentLabel = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.paymentComboBox = new System.Windows.Forms.ComboBox();
            this.productInfoLabel = new System.Windows.Forms.Label();
            this.clientDetailsTextBox = new System.Windows.Forms.TextBox();
            this.clientDetailsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // returnBackButton
            // 
            this.returnBackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.returnBackButton.Location = new System.Drawing.Point(12, 201);
            this.returnBackButton.Name = "returnBackButton";
            this.returnBackButton.Size = new System.Drawing.Size(75, 37);
            this.returnBackButton.TabIndex = 11;
            this.returnBackButton.Text = "<";
            this.returnBackButton.UseVisualStyleBackColor = true;
            this.returnBackButton.Click += new System.EventHandler(this.returnBackButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(44, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Заполни данные:";
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(90, 41);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(41, 13);
            this.addressLabel.TabIndex = 13;
            this.addressLabel.Text = "Адрес:";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(93, 57);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(121, 20);
            this.addressTextBox.TabIndex = 14;
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Location = new System.Drawing.Point(90, 80);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(96, 13);
            this.phoneLabel.TabIndex = 15;
            this.phoneLabel.Text = "Номер телефона:";
            // 
            // paymentLabel
            // 
            this.paymentLabel.AutoSize = true;
            this.paymentLabel.Location = new System.Drawing.Point(90, 119);
            this.paymentLabel.Name = "paymentLabel";
            this.paymentLabel.Size = new System.Drawing.Size(87, 13);
            this.paymentLabel.TabIndex = 16;
            this.paymentLabel.Text = "Способ оплаты:";
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(93, 201);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(121, 37);
            this.confirmButton.TabIndex = 17;
            this.confirmButton.Text = "Подтвердить заказ";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Location = new System.Drawing.Point(93, 96);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.ReadOnly = true;
            this.phoneTextBox.Size = new System.Drawing.Size(121, 20);
            this.phoneTextBox.TabIndex = 18;
            // 
            // paymentComboBox
            // 
            this.paymentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paymentComboBox.FormattingEnabled = true;
            this.paymentComboBox.Location = new System.Drawing.Point(93, 135);
            this.paymentComboBox.Name = "paymentComboBox";
            this.paymentComboBox.Size = new System.Drawing.Size(121, 21);
            this.paymentComboBox.TabIndex = 19;
            // 
            // productInfoLabel
            // 
            this.productInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.productInfoLabel.Location = new System.Drawing.Point(220, 57);
            this.productInfoLabel.Name = "productInfoLabel";
            this.productInfoLabel.Size = new System.Drawing.Size(199, 181);
            this.productInfoLabel.TabIndex = 20;
            // 
            // clientDetailsTextBox
            // 
            this.clientDetailsTextBox.Location = new System.Drawing.Point(93, 175);
            this.clientDetailsTextBox.Name = "clientDetailsTextBox";
            this.clientDetailsTextBox.Size = new System.Drawing.Size(121, 20);
            this.clientDetailsTextBox.TabIndex = 21;
            // 
            // clientDetailsLabel
            // 
            this.clientDetailsLabel.AutoSize = true;
            this.clientDetailsLabel.Location = new System.Drawing.Point(90, 159);
            this.clientDetailsLabel.Name = "clientDetailsLabel";
            this.clientDetailsLabel.Size = new System.Drawing.Size(95, 13);
            this.clientDetailsLabel.TabIndex = 22;
            this.clientDetailsLabel.Text = "Детали к заказу:";
            // 
            // Ordering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 261);
            this.Controls.Add(this.clientDetailsLabel);
            this.Controls.Add(this.clientDetailsTextBox);
            this.Controls.Add(this.productInfoLabel);
            this.Controls.Add(this.paymentComboBox);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.paymentLabel);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.returnBackButton);
            this.MaximumSize = new System.Drawing.Size(460, 300);
            this.MinimumSize = new System.Drawing.Size(460, 300);
            this.Name = "Ordering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ordering";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ordering_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button returnBackButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.Label paymentLabel;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.ComboBox paymentComboBox;
        private System.Windows.Forms.Label productInfoLabel;
        private System.Windows.Forms.TextBox clientDetailsTextBox;
        private System.Windows.Forms.Label clientDetailsLabel;
    }
}