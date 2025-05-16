using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using MySql.Data.MySqlClient;

namespace Melo_ITSolutions
{
    public partial class ChangePasswordForm : Form
    {
        private string userEmail;
        public ChangePasswordForm(string email)
        {
            InitializeComponent();
            userEmail = email;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newPassword = textBox1.Text.Trim();
            string confirmPassword = textBox2.Text.Trim();

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=melo_itsolutions;"))
            {
                conn.Open();
                string query = "UPDATE admin SET password = @Password, otp_code = NULL WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Password", newPassword); // Hashing recommended!
                cmd.Parameters.AddWithValue("@Email", userEmail);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Password changed successfully!");
                    this.Close();
                    Form1 form = new Form1();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Error updating password.");
                }
            }
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
