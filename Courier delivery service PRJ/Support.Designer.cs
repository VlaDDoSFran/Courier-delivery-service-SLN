namespace Courier_delivery_service_PRJ
{
    partial class Support
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelChat = new System.Windows.Forms.Panel();
            this.chatList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelInput = new System.Windows.Forms.Panel();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnTestAPI = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelSupportInfo = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelChat.SuspendLayout();
            this.panelInput.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Controls.Add(this.lblStatus);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(2);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(738, 65);
            this.panelTop.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(15, 12);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(339, 26);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Поддержка службы доставки";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(450, 12);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(273, 41);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Проверка подключения...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelChat
            // 
            this.panelChat.Controls.Add(this.chatList);
            this.panelChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChat.Location = new System.Drawing.Point(0, 65);
            this.panelChat.Margin = new System.Windows.Forms.Padding(2);
            this.panelChat.Name = "panelChat";
            this.panelChat.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.panelChat.Size = new System.Drawing.Size(738, 423);
            this.panelChat.TabIndex = 1;
            // 
            // chatList
            // 
            this.chatList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.chatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.chatList.HideSelection = false;
            this.chatList.Location = new System.Drawing.Point(15, 16);
            this.chatList.Margin = new System.Windows.Forms.Padding(2);
            this.chatList.Name = "chatList";
            this.chatList.Size = new System.Drawing.Size(708, 391);
            this.chatList.TabIndex = 0;
            this.chatList.UseCompatibleStateImageBehavior = false;
            this.chatList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Чат";
            this.columnHeader1.Width = 900;
            // 
            // panelInput
            // 
            this.panelInput.Controls.Add(this.txtQuestion);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInput.Location = new System.Drawing.Point(0, 488);
            this.panelInput.Margin = new System.Windows.Forms.Padding(2);
            this.panelInput.Name = "panelInput";
            this.panelInput.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.panelInput.Size = new System.Drawing.Size(738, 65);
            this.panelInput.TabIndex = 2;
            // 
            // txtQuestion
            // 
            this.txtQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuestion.Location = new System.Drawing.Point(15, 8);
            this.txtQuestion.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(708, 49);
            this.txtQuestion.TabIndex = 0;
            this.txtQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuestion_KeyDown);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.exitButton);
            this.panelButtons.Controls.Add(this.btnSend);
            this.panelButtons.Controls.Add(this.btnClear);
            this.panelButtons.Controls.Add(this.btnTestAPI);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 553);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(2);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.panelButtons.Size = new System.Drawing.Size(738, 57);
            this.panelButtons.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(580, 8);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(142, 41);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(15, 8);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(135, 41);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Очистить чат";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnTestAPI
            // 
            this.btnTestAPI.Location = new System.Drawing.Point(165, 8);
            this.btnTestAPI.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestAPI.Name = "btnTestAPI";
            this.btnTestAPI.Size = new System.Drawing.Size(135, 41);
            this.btnTestAPI.TabIndex = 3;
            this.btnTestAPI.Text = "Проверить API";
            this.btnTestAPI.UseVisualStyleBackColor = true;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.labelSupportInfo);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 610);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(2);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(738, 32);
            this.panelBottom.TabIndex = 4;
            // 
            // labelSupportInfo
            // 
            this.labelSupportInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSupportInfo.Location = new System.Drawing.Point(0, 0);
            this.labelSupportInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSupportInfo.Name = "labelSupportInfo";
            this.labelSupportInfo.Size = new System.Drawing.Size(738, 32);
            this.labelSupportInfo.TabIndex = 0;
            this.labelSupportInfo.Text = "Телефон поддержки: 8-800-555-35-35 | Работаем: ежедневно с 8:00 до 22:00 | Сайт: " +
    "https://fast-delivery.ru";
            this.labelSupportInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(315, 8);
            this.exitButton.Margin = new System.Windows.Forms.Padding(2);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(135, 41);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Выйти";
            this.exitButton.UseVisualStyleBackColor = true;
            // 
            // Support
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 642);
            this.Controls.Add(this.panelChat);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelBottom);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(754, 681);
            this.Name = "Support";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поддержка службы доставки";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Support_FormClosed);
            this.Load += new System.EventHandler(this.Support_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelChat.ResumeLayout(false);
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelChat;
        private System.Windows.Forms.ListView chatList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnTestAPI;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label labelSupportInfo;
        private System.Windows.Forms.Button exitButton;
    }
}