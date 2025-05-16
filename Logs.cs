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
    public partial class Logs : Form
    {
        public Logs()
        {
            InitializeComponent();
            this.Load += Logs_Load; // Ensure form load is wired up
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            LoadLogs(); // Load departments on form load
        }

        private void LoadLogs()
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM logs ORDER BY log_id ASC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    // Check if data is loaded
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No logs found.");
                    }

                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns[2].Width = 150;
                    dataGridView1.Columns[3].Width = 150;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading logs: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Departments departments = new Departments();
            departments.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();   
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
