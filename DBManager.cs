// DatabaseManager.cs
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Melo_ITSolutions
{
    public class DatabaseManager
    {
        private string connectionString = "server=localhost;user=root;password=;database=melo_itsolutions";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public bool Login(string email, string password)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM admin WHERE password=@Password AND email=@Email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        public bool SignUp(string name, string email, string password)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO admin (name, email, password) VALUES (@Name, @Email, @Password)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }

        }
    }
}
