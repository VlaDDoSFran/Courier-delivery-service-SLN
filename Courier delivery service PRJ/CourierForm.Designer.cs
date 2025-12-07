using System.Windows.Forms;

namespace Courier_delivery_service_PRJ
{
    partial class CourierForm
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
            this.flowLayoutPanelOrders = new System.Windows.Forms.FlowLayoutPanel();
            this.exitButton = new System.Windows.Forms.Button();
            this.refreshOrdersButton = new System.Windows.Forms.Button();
            this.balanceLabel = new System.Windows.Forms.Label();
            this.sendMoneyButton = new System.Windows.Forms.Button();
            this.supportButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanelOrders
            // 
            this.flowLayoutPanelOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelOrders.AutoScroll = true;
            this.flowLayoutPanelOrders.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanelOrders.Name = "flowLayoutPanelOrders";
            this.flowLayoutPanelOrders.Size = new System.Drawing.Size(760, 607);
            this.flowLayoutPanelOrders.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitButton.Location = new System.Drawing.Point(897, 8);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 30);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "ВЫЙТИ";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // refreshOrdersButton
            // 
            this.refreshOrdersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshOrdersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.refreshOrdersButton.Location = new System.Drawing.Point(865, 44);
            this.refreshOrdersButton.Name = "refreshOrdersButton";
            this.refreshOrdersButton.Size = new System.Drawing.Size(107, 55);
            this.refreshOrdersButton.TabIndex = 8;
            this.refreshOrdersButton.Text = "Обновить список заказов";
            this.refreshOrdersButton.UseVisualStyleBackColor = true;
            this.refreshOrdersButton.Click += new System.EventHandler(this.refreshOrdersButton_Click);
            // 
            // balanceLabel
            // 
            this.balanceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.balanceLabel.AutoSize = true;
            this.balanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.balanceLabel.Location = new System.Drawing.Point(778, 310);
            this.balanceLabel.Name = "balanceLabel";
            this.balanceLabel.Size = new System.Drawing.Size(94, 24);
            this.balanceLabel.TabIndex = 9;
            this.balanceLabel.Text = "Баланс: 0";
            // 
            // sendMoneyButton
            // 
            this.sendMoneyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMoneyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendMoneyButton.Location = new System.Drawing.Point(865, 105);
            this.sendMoneyButton.Name = "sendMoneyButton";
            this.sendMoneyButton.Size = new System.Drawing.Size(107, 55);
            this.sendMoneyButton.TabIndex = 11;
            this.sendMoneyButton.Text = "Перевести деньги";
            this.sendMoneyButton.UseVisualStyleBackColor = true;
            this.sendMoneyButton.Click += new System.EventHandler(this.sendMoneyButton_Click);
            // 
            // supportButton
            // 
            this.supportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.supportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.supportButton.Location = new System.Drawing.Point(865, 166);
            this.supportButton.Name = "supportButton";
            this.supportButton.Size = new System.Drawing.Size(107, 55);
            this.supportButton.TabIndex = 13;
            this.supportButton.Text = "Тех.поддержка";
            this.supportButton.UseVisualStyleBackColor = true;
            this.supportButton.Click += new System.EventHandler(this.supportButton_Click);
            // 
            // CourierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 631);
            this.Controls.Add(this.supportButton);
            this.Controls.Add(this.sendMoneyButton);
            this.Controls.Add(this.balanceLabel);
            this.Controls.Add(this.refreshOrdersButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.flowLayoutPanelOrders);
            this.MinimumSize = new System.Drawing.Size(1000, 670);
            this.Name = "CourierForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CourierForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CourierForm_FormClosed);
            this.Load += new System.EventHandler(this.CourierForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelOrders;
        private Button exitButton;
        private Button refreshOrdersButton;
        private Label balanceLabel;
        private Button sendMoneyButton;
        private Button supportButton;
    }
}