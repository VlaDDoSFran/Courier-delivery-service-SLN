namespace Courier_delivery_service_PRJ
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SignUpButton = new System.Windows.Forms.Button();
            this.EntryLabel = new System.Windows.Forms.Label();
            this.SignInButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SignUpButton
            // 
            this.SignUpButton.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SignUpButton.Location = new System.Drawing.Point(276, 192);
            this.SignUpButton.Name = "SignUpButton";
            this.SignUpButton.Size = new System.Drawing.Size(206, 60);
            this.SignUpButton.TabIndex = 0;
            this.SignUpButton.Text = "Регистрация";
            this.SignUpButton.UseVisualStyleBackColor = true;
            this.SignUpButton.Click += new System.EventHandler(this.SignUpButton_Click);
            // 
            // EntryLabel
            // 
            this.EntryLabel.Font = new System.Drawing.Font("Rubik Black", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EntryLabel.Location = new System.Drawing.Point(120, 9);
            this.EntryLabel.Name = "EntryLabel";
            this.EntryLabel.Size = new System.Drawing.Size(611, 165);
            this.EntryLabel.TabIndex = 1;
            this.EntryLabel.Text = "Добро пожаловать в наше приложение!";
            // 
            // SignInButton
            // 
            this.SignInButton.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SignInButton.Location = new System.Drawing.Point(276, 270);
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.Size = new System.Drawing.Size(206, 60);
            this.SignInButton.TabIndex = 3;
            this.SignInButton.Text = "Вход";
            this.SignInButton.UseVisualStyleBackColor = true;
            this.SignInButton.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 461);
            this.Controls.Add(this.SignInButton);
            this.Controls.Add(this.EntryLabel);
            this.Controls.Add(this.SignUpButton);
            this.MaximumSize = new System.Drawing.Size(820, 500);
            this.MinimumSize = new System.Drawing.Size(820, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SignUpButton;
        private System.Windows.Forms.Label EntryLabel;
        private System.Windows.Forms.Button SignInButton;
    }
}

