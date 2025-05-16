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
    public partial class UpdateDepartmentForm : Form
    {
        public int DepartmentID { get; set; }
        public string DepartmentName
        {
            get => deptName.Text;
            set => deptName.Text = value;
        }
        public UpdateDepartmentForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Update logic here (or raise an event to call from the main form)
            string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions;";
            string query = "UPDATE departments SET department_name = @name WHERE department_id = @id";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", deptName.Text);
                cmd.Parameters.AddWithValue("@id", DepartmentID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
