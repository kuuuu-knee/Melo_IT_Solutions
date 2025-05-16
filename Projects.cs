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
using Excel = Microsoft.Office.Interop.Excel;

namespace Melo_ITSolutions
{
    public partial class Projects : Form
    {
        public Projects()
        {
            InitializeComponent();
            this.Load += Projects_Load; // Ensure form load is wired up
        }

        private void Projects_Load(object sender, EventArgs e)
        {
            LoadProjects(); // Load departments on form load
        }

        private void ExportProjectsToExcel()
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "Projects Report";

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

                worksheet.Columns.AutoFit();
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message);
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
                    string query = "SELECT * FROM projects ORDER BY project_id ASC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    // Check if data is loaded
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No projects found.");
                    }

                    dataGridView1.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading projects: " + ex.Message);
                }
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Tasks tasks = new Tasks();  
            tasks.Show();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddProjectForm addUserForm = new AddProjectForm();
            addUserForm.FormClosed += (s, args) => LoadProjects();
            addUserForm.ShowDialog(); 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                int id = Convert.ToInt32(row.Cells["project_id"].Value);
                string name = row.Cells["project_name"].Value.ToString();
                DateTime start_date = Convert.ToDateTime(row.Cells["start_date"].Value);
                DateTime end_date = Convert.ToDateTime(row.Cells["end_date"].Value);
                int departmentId = Convert.ToInt32(row.Cells["department_id"].Value);

                // Open the Update User form and pass data
                var updateForm = new UpdateProjectForm();
                updateForm.ProjectID = id;
                updateForm.ProjectName = name;
                updateForm.Project_Start_Date = start_date;
                updateForm.Project_End_Date = end_date;
                updateForm.Project_Dept = departmentId;

                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    LoadProjects(); // Refresh DataGridView after update
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

                int projectID = Convert.ToInt32(row.Cells["project_id"].Value);

                var confirmResult = MessageBox.Show("Are you sure you want to delete this project?",
                                                     "Confirm Delete",
                                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    
                    string query = "DELETE FROM projects WHERE project_id = @id";

                    try
                    {
                        // Replace with your actual connection string
                        string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", projectID);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        // Reload departments to refresh the DataGridView
                        LoadProjects();

                        MessageBox.Show("Project deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting project: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a project to delete.");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ExportProjectsToExcel();
        }
    }
}
