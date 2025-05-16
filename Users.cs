using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;


namespace Melo_ITSolutions
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            this.Load += Users_Load;
        }
        private void Users_Load(object sender, EventArgs e)
        {
            LoadUsers(); // Load departments on form load
        }
        private void LoadUsers()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM users";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    // Check if data is loaded
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No users found.");
                    }

                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns[0].Width = 150;
                    dataGridView1.Columns[1].Width = 150;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading users: " + ex.Message);
                }
            }
        }

        private void ExportToExcel()
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "Users Report";

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


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Projects projects = new Projects();
            projects.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Departments departments = new Departments();   
            departments.Show();
            this.Hide();
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
            AddUserForm addUserForm = new AddUserForm();
            addUserForm.FormClosed += (s, args) => LoadUsers(); // Reload after closing AddUser
            addUserForm.ShowDialog(); // Modal, prevents duplicates
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Get the department ID of the selected row
                int userID = Convert.ToInt32(row.Cells["user_id"].Value);

                // Ask for confirmation before deleting
                var confirmResult = MessageBox.Show("Are you sure you want to delete this user?",
                                                     "Confirm Delete",
                                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // Execute delete query
                    string query = "DELETE FROM users WHERE user_id = @id";

                    try
                    {
                        // Replace with your actual connection string
                        string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", userID);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        // Reload departments to refresh the DataGridView
                        LoadUsers();

                        MessageBox.Show("User deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting user: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                int id = Convert.ToInt32(row.Cells["user_id"].Value);
                string name = row.Cells["name"].Value.ToString();
                string email = row.Cells["email"].Value.ToString();
                string password = row.Cells["password"].Value.ToString();
                int roleId = Convert.ToInt32(row.Cells["role_id"].Value); // now treated as int
                int departmentId = Convert.ToInt32(row.Cells["department_id"].Value);

                // Open the Update User form and pass data
                var updateForm = new UpdateUsersForm();
                updateForm.UserID = id;
                updateForm.UserName = name;
                updateForm.UserEmail = email;
                updateForm.UserPassword = password;
                updateForm.UserRole = roleId;            // changed to match int role
                updateForm.UserDept = departmentId;

                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers(); // Refresh DataGridView after update
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
}
