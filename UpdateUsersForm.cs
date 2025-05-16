using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Melo_ITSolutions
{
    public partial class UpdateUsersForm : Form
    {
        public int UserID { get; set; }
        public string UserName 
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }
        public string UserEmail
        {
            get => txtEmail.Text;
            set => txtEmail.Text = value;
        }
        public string UserPassword
        {
            get => txtPassword.Text;
            set => txtPassword.Text = value;
        }
        public int UserRole
        {
            get => Convert.ToInt32(comboBoxRole.SelectedValue);
            set => comboBoxRole.SelectedValue = value;
        }
        public int UserDept
        {
            get => Convert.ToInt32(comboBoxDept.SelectedValue);
            set => comboBoxDept.SelectedValue = value;
        }

        public UpdateUsersForm()
        {
            InitializeComponent();
        }

        private void UpdateUsersForm_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadRoles();
        }


        private void LoadDepartments()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT department_id, department_name FROM departments";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxDept.DisplayMember = "department_name";
                    comboBoxDept.ValueMember = "department_id";
                    comboBoxDept.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading departments: " + ex.Message);
                }
            }
        }

        private void LoadRoles()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT role_id, role_name FROM roles";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxRole.DisplayMember = "role_name";
                    comboBoxRole.ValueMember = "role_id";
                    comboBoxRole.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading roles: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            string query = @"UPDATE users 
                 SET name = @name, 
                     email = @email, 
                     password = @password, 
                     role_id = @role, 
                     department_id = @department 
                 WHERE user_id = @id";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", UserName);
                cmd.Parameters.AddWithValue("@email", UserEmail);
                cmd.Parameters.AddWithValue("@password", UserPassword);
                cmd.Parameters.AddWithValue("@role", UserRole);
                cmd.Parameters.AddWithValue("@department", UserDept);
                cmd.Parameters.AddWithValue("@id", UserID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
