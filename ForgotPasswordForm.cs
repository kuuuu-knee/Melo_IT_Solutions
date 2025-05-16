using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Melo_ITSolutions
{
    public partial class ForgotPasswordForm : Form
    {
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Button button1;

        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(159, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHANGE YOUR PASSWORD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(159, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter your Email:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(162, 186);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(350, 30);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(205, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(245, 51);
            this.button1.TabIndex = 3;
            this.button1.Text = "SEND TO EMAIL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ForgotPasswordForm
            // 
            this.ClientSize = new System.Drawing.Size(637, 420);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ForgotPasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ForgotPasswordForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Enter your registered email.");
                return;
            }

            string otp = new Random().Next(100000, 999999).ToString();

            // Save OTP to DB or memory (you can use a separate OTP table or temporary memory)
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=melo_itsolutions;"))
            {
                conn.Open();
                string query = "UPDATE admin SET otp_code = @otp WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@otp", otp);
                cmd.Parameters.AddWithValue("@Email", email);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    // Send email
                    SendOTPEmail(email, otp);
                    OTPVerificationForm otpForm = new OTPVerificationForm(email);
                    otpForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email not found.");
                }
            }
        }

        private void SendOTPEmail(string email, string otp)
        {
            try
            {
                MailMessage msg = new MailMessage("clementeneco7@gmail.com", email, "Password Reset OTP", $"Your OTP is: {otp}");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("clementeneco7@gmail.com", "qcasixnyogwrsvlo");
                client.EnableSsl = true;
                client.Send(msg);
                MessageBox.Show("OTP sent successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending OTP: " + ex.Message);
            }
        }

        private void ForgotPasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
