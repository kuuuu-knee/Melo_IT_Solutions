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
    public partial class UpdateTaskForm : Form
    {
        public int TaskID { get; set; }

        public string TaskName { get; set; }

        public int Assigned_to { get; set; }

        public int Project_ID { get; set; } 

        public string Status { get; set; }
        public UpdateTaskForm()
        {
            InitializeComponent();
            this.Load += UpdateTaskForm_Load;
        }

        private void UpdateTaskForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadProjects();
            LoadEnumStatus();

            if (TaskID > 0)
            {
                LoadTaskDetails();
            }
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
                    MessageBox.Show("Error loading users: " + ex.Message);
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

        private void LoadEnumStatus()
        {
            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.AddRange(new string[] { "Pending", "In Progress", "Completed" });
        }

        private void LoadTaskDetails()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT task_name, assigned_to, project_id, status FROM tasks WHERE task_id = @task_id LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@task_id", TaskID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = reader["task_name"].ToString();

                            int assignedTo = Convert.ToInt32(reader["assigned_to"]);
                            int projectId = Convert.ToInt32(reader["project_id"]);
                            string status = reader["status"].ToString();

                            comboBoxUser.SelectedValue = assignedTo;
                            comboBoxProj.SelectedValue = projectId;
                            comboBoxStatus.SelectedItem = status;
                        }
                        else
                        {
                            MessageBox.Show("Task not found.");
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading task details: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string taskName = textBox1.Text.Trim();
            int assignedTo = Convert.ToInt32(comboBoxUser.SelectedValue);
            int projectId = Convert.ToInt32(comboBoxProj.SelectedValue);
            string status = comboBoxStatus.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(taskName))
            {
                MessageBox.Show("Task name is required.");
                return;
            }
            if (string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE tasks 
                                     SET task_name = @task_name, 
                                         assigned_to = @assigned_to, 
                                         project_id = @project_id, 
                                         status = @status
                                     WHERE task_id = @task_id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@task_name", taskName);
                    cmd.Parameters.AddWithValue("@assigned_to", assignedTo);
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@task_id", TaskID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Task updated successfully!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating task: " + ex.Message);
                }
            }
        }
    }
}
