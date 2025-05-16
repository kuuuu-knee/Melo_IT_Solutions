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
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Melo_ITSolutions
{
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
            this.Load += Tasks_Load; // Ensure form load is wired up
        }

        private void Tasks_Load(object sender, EventArgs e)
        {
            LoadTasks(); // Load departments on form load
        }

        private void LoadTasks()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM tasks ORDER BY task_id ASC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    // Check if data is loaded
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No tasks found.");
                    }

                    dataGridView1.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tasks: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Departments departments = new Departments();
            departments.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Projects projects = new Projects();
            projects.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Logs logs = new Logs();
            logs.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void ExportToExcel()
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "Tasks Report";

                // Add column headers
                for (int i = 1; i <= dataGridView1.Columns.Count; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                // Add rows
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Apply auto-filter to headers
                Excel.Range headerRange = worksheet.Range[
                    worksheet.Cells[1, 1],
                    worksheet.Cells[1, dataGridView1.Columns.Count]
                ];
                headerRange.AutoFilter(1);

                // Auto fit columns
                worksheet.Columns.AutoFit();

                // Show Excel
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export to Excel failed: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddTaskForm addTaskForm = new AddTaskForm();
            addTaskForm.FormClosed += (s, args) => LoadTasks(); // Reload after closing AddUser
            addTaskForm.ShowDialog(); // Modal, prevents duplicates
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                int id = Convert.ToInt32(row.Cells["task_id"].Value);
                string name = row.Cells["task_name"].Value.ToString();
                int assigned_to  = Convert.ToInt32(row.Cells["assigned_to"].Value);
                int project_id = Convert.ToInt32(row.Cells["project_id"].Value); ;
                string status = row.Cells["status"].Value.ToString();

                // Open the Update User form and pass data
                var updateForm = new UpdateTaskForm();
                updateForm.TaskID = id;
                updateForm.TaskName = name;
                updateForm.Assigned_to = assigned_to;
                updateForm.Project_ID = project_id;
                updateForm.Status = status;

                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    LoadTasks(); // Refresh DataGridView after update
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Get the department ID of the selected row
                int taskID = Convert.ToInt32(row.Cells["task_id"].Value);

                // Ask for confirmation before deleting
                var confirmResult = MessageBox.Show("Are you sure you want to delete this task?",
                                                     "Confirm Delete",
                                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // Execute delete query
                    string query = "DELETE FROM tasks WHERE task_id = @id";

                    try
                    {
                        // Replace with your actual connection string
                        string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", taskID);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        // Reload departments to refresh the DataGridView
                        LoadTasks();

                        MessageBox.Show("Tasl deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting task: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
}
