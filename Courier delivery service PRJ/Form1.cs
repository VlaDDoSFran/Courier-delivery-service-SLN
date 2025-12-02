using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Courier_delivery_service_PRJ
{
    public partial class Form1 : Form
    {
        public SignInForm signIn { get; set; }
        public SignUpForm signUp { get; set; }
        string connStr = @"Data Source = DESKTOP-O03Q1EM; Initial Catalog=Courier_delivery_service;Integrated Security = True";
        public Form1()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            if (signUp != null)
            {
                signUp.Show();
            }
            else
            {
                SignUpForm signUpForm = new SignUpForm();
                signUpForm.form1 = this;
                signUpForm.Show();
                signUp = signUpForm;
            }
            this.Hide();
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            if (signIn != null)
            {
                signIn.Show();
            }
            else
            {
                SignInForm signInForm = new SignInForm();
                signInForm.form1 = this;
                signInForm.Show();
                signIn = signInForm;
            }
            this.Hide();
        }
    }
}
