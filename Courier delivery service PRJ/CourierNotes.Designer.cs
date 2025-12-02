namespace Courier_delivery_service_PRJ
{
    partial class CourierNotes
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
            this.OkButton = new System.Windows.Forms.Button();
            this.notesLabel = new System.Windows.Forms.Label();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OkButton.Location = new System.Drawing.Point(76, 240);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(169, 48);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "Подтвердить";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // notesLabel
            // 
            this.notesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.notesLabel.Location = new System.Drawing.Point(12, 9);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(313, 23);
            this.notesLabel.TabIndex = 1;
            this.notesLabel.Text = "Ваши заметки пользователю:";
            // 
            // notesTextBox
            // 
            this.notesTextBox.Location = new System.Drawing.Point(17, 50);
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.Size = new System.Drawing.Size(308, 176);
            this.notesTextBox.TabIndex = 2;
            // 
            // CourierNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 301);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.notesLabel);
            this.Controls.Add(this.OkButton);
            this.MaximumSize = new System.Drawing.Size(360, 340);
            this.MinimumSize = new System.Drawing.Size(360, 340);
            this.Name = "CourierNotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CourierNotes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CourierNotes_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.TextBox notesTextBox;
    }
}