using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Melo_ITSolutions
{
    public partial class AddTaskForm : Form
    {
        public AddTaskForm()
        {
            InitializeComponent();
        }

        private void AddTaskForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadProjects();
        }

        private void LoadUsers()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT user_id, name FROM users";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxUser.DisplayMember = "name";
                    comboBoxUser.ValueMember = "user_id";
                    comboBoxUser.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading departments: " + ex.Message);
                }
            }
        }

        private void LoadProjects()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT project_id, project_name FROM projects";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxProj.DisplayMember = "project_name";
                    comboBoxProj.ValueMember = "project_id";
                    comboBoxProj.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading projects: " + ex.Message);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            int assigned_to = Convert.ToInt32(comboBoxUser.SelectedValue);
            int project_id = Convert.ToInt32(comboBoxProj.SelectedValue);
            string status = "Pending";

            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO tasks (task_name, assigned_to, project_id, status) VALUES (@name, @assigned_to, @project_id, @status)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@assigned_to", assigned_to);
                    cmd.Parameters.AddWithValue("@project_id", project_id);
                    cmd.Parameters.AddWithValue("@status", status);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Task added successfully!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
