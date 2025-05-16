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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Melo_ITSolutions
{
    public partial class UpdateProjectForm : Form
    {
        public int ProjectID { get; set; }
        public string ProjectName
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }
        public DateTime Project_Start_Date
        {
            get => dateTimePicker1.Value;
            set => dateTimePicker1.Value = value;
        }
        public DateTime Project_End_Date
        {
            get => dateTimePicker2.Value;
            set => dateTimePicker2.Value = value;
        }
        public int Project_Dept
        {
            get => Convert.ToInt32(comboBoxDept.SelectedValue);
            set => comboBoxDept.SelectedValue = value;
        }

        public UpdateProjectForm()
        {
            InitializeComponent();
            this.Load += UpdateProjectForm_Load;
        }

        private void UpdateProjectForm_Load(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            string query = @"UPDATE projects 
                 SET project_name = @name, 
                     start_date = @start_date, 
                     end_date = @end_date, 
                     department_id = @department 
                 WHERE project_id = @id";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", ProjectName);
                cmd.Parameters.AddWithValue("@start_date", Project_Start_Date);
                cmd.Parameters.AddWithValue("@end_date", Project_End_Date);
                cmd.Parameters.AddWithValue("@department", Project_Dept);
                cmd.Parameters.AddWithValue("@id", ProjectID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
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

        private void comboBoxDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
