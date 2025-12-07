using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Courier_delivery_service_PRJ
{
    public partial class CourierNotes : Form
    {
        public Form1 form1 { get; set; }
        public CourierForm courier { get; set; }
        public string notes = null;
        public int OrderId;
        public CourierNotes()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(notesTextBox.Text))
            {
                MessageBox.Show("Поле пустое! Заполните его.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                notes = notesTextBox.Text;
                this.Hide();
                courier.Show();
                courier.CompleteOrderAcceptance(OrderId, notes);
                //this.Close();
            }
        }

        private void CourierNotes_FormClosed(object sender, FormClosedEventArgs e)
        {
            courier?.Show();
        }
    }
}
