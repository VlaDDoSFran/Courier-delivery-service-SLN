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
            this.flowLayoutPanelOrders.Size = new System.Drawing.Size(775, 607);
            this.flowLayoutPanelOrders.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitButton.Location = new System.Drawing.Point(857, 8);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 30);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "ВЫЙТИ";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // refreshOrdersButton
            // 
            this.refreshOrdersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.refreshOrdersButton.Location = new System.Drawing.Point(825, 44);
            this.refreshOrdersButton.Name = "refreshOrdersButton";
            this.refreshOrdersButton.Size = new System.Drawing.Size(107, 55);
            this.refreshOrdersButton.TabIndex = 8;
            this.refreshOrdersButton.Text = "Обновить список заказов";
            this.refreshOrdersButton.UseVisualStyleBackColor = true;
            this.refreshOrdersButton.Click += new System.EventHandler(this.refreshOrdersButton_Click);
            // 
            // CourierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 631);
            this.Controls.Add(this.refreshOrdersButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.flowLayoutPanelOrders);
            this.MinimumSize = new System.Drawing.Size(960, 670);
            this.Name = "CourierForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CourierForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CourierForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelOrders;
        private Button exitButton;
        private Button refreshOrdersButton;
    }
}