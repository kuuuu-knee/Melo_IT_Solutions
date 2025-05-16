using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Melo_ITSolutions
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string email = textBox3.Text.Trim();
            string password = textBox2.Text.Trim();
           


            DatabaseManager db = new DatabaseManager();

            bool success = db.SignUp(name, email, password);

            if (success)
            {
                MessageBox.Show("Sign up successful! You can now log in.");
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sign up failed. Email might already be in use.");
            }
        }

        // These are optional event handlers, keep if needed
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
