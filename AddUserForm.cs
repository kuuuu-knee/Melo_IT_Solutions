using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Melo_ITSolutions
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddUserForm_Load(object sender, EventArgs e)
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


        private void button2_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim(); 
            int roleId = Convert.ToInt32(comboBoxRole.SelectedValue);
            int departmentId = Convert.ToInt32(comboBoxDept.SelectedValue);

            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO users (name, password, email, role_id, department_id) VALUES (@name, @password, @email, @role_id, @department_id)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@role_id", roleId);
                    cmd.Parameters.AddWithValue("@department_id", departmentId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User added successfully!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
            this.Hide();
        }
    }
}
