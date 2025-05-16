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
    public partial class OTPVerificationForm : Form
    {
        private string userEmail;
        public OTPVerificationForm(string email)
        {
            InitializeComponent();
            userEmail = email;
        }

        private void OTPVerificationForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputOTP = textBox1.Text.Trim();

            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=melo_itsolutions;"))
            {
                conn.Open();
                string query = "SELECT otp_code FROM admin WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", userEmail);

                string storedOTP = Convert.ToString(cmd.ExecuteScalar());

                if (storedOTP == inputOTP)
                {
                    ChangePasswordForm changeForm = new ChangePasswordForm(userEmail);
                    changeForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid OTP.");
                }
            }
        }
    }
}
