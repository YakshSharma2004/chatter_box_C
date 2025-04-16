using Microsoft.Data.SqlClient;
using System.Windows;

namespace sql_practice
{
    // This class manages the SQL Server database connection.
    public class Sqlconnection
    {
        // Connection string used to establish the SQL connection.
        private readonly string connectionString;

        // Constructor initializes the class with the provided connection string.
        public Sqlconnection(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // This method creates a new SqlConnection instance, attempts to open it,
        // and returns the open connection. If an error occurs, it shows the exception message.
        public SqlConnection Getconn()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open(); // Try to open the connection.
                return conn; // Return the open connection.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Display error if connection fails.
                conn.Close(); // Ensure the connection is closed on failure.
                return null; // Return null to indicate failure.
            }
        }

        // This method safely closes the given SqlConnection instance.
        public void closeConn(SqlConnection con)
        {
            con.Close();
        }
    }
}