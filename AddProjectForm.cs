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
    public partial class AddProjectForm : Form
    {
        public AddProjectForm()
        {
            InitializeComponent();
            this.Load += AddProjectForm_Load;
        }

        public void AddProjectForm_Load(object sender, EventArgs e)
        {
            LoadDepartments();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            DateTime start_date = dateTimePicker1.Value;
            DateTime end_date = dateTimePicker2.Value;
            int departmentId = Convert.ToInt32(comboBoxDept.SelectedValue);

            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO projects (project_name, start_date, end_date, department_id) VALUES (@name, @start_date, @end_date,@department_id)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@start_date", start_date);
                    cmd.Parameters.AddWithValue("@end_date", end_date);
                    cmd.Parameters.AddWithValue("@department_id", departmentId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Project added successfully!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void AddProjectForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
