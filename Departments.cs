using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Melo_ITSolutions
{
    public partial class Departments : Form
    {
        public Departments()
        {
            InitializeComponent();
            this.Load += Departments_Load; // Ensure form load is wired up
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            LoadDepartments(); // Load departments on form load
        }

        private void LoadDepartments()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM departments ORDER BY department_id ASC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    // Check if data is loaded
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No departments found.");
                    }

                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns[0].Width = 240;
                    dataGridView1.Columns[1].Width = 240;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading departments: " + ex.Message);
                }
            }
        }


        private void label2_Click(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Projects projects = new Projects();
            projects.Show();
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

        private void groupBox2_Enter(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                int id = Convert.ToInt32(row.Cells["department_id"].Value);
                string name = row.Cells["department_name"].Value.ToString();

                var updateForm = new UpdateDepartmentForm();
                updateForm.DepartmentID = id;
                updateForm.DepartmentName = name;

                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDepartments(); // Refresh DataGridView
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
                int departmentID = Convert.ToInt32(row.Cells["department_id"].Value);

                // Ask for confirmation before deleting
                var confirmResult = MessageBox.Show("Are you sure you want to delete this department?",
                                                     "Confirm Delete",
                                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // Execute delete query
                    string query = "DELETE FROM departments WHERE department_id = @id";

                    try
                    {
                        // Replace with your actual connection string
                        string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", departmentID);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        // Reload departments to refresh the DataGridView
                        LoadDepartments();

                        MessageBox.Show("Department deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting department: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a department to delete.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddDepartmentForm addUserForm = new AddDepartmentForm();
            addUserForm.FormClosed += (s, args) => LoadDepartments(); // Reload after closing AddUser
            addUserForm.ShowDialog(); // Modal, prevents duplicates
        }
    }
}
