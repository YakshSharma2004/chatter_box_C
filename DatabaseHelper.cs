using Microsoft.Data.SqlClient;

namespace sql_practice
{
    internal class DatabaseHelper
    {
        private Sqlconnection conn;
        private List<user> users;
        private SqlConnection connection;

        //public DatabaseHelper(Sqlconnection conn) 
        //{
        //    this.conn = conn;
        //}

        public DatabaseHelper(SqlConnection connection)
        {
            this.connection = connection;
        }

        private void LoadUsers()
        {
            //SqlConnection connection=conn.Getconn();
            users = new List<user>();
            string query = "SELECT user_id, user_name, user_pass, UserDescription, DateOfJoin FROM [dbo].[user];";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new user
                        {
                            UserId = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            UserDescription = reader.GetString(3),
                            datejoin = (DateOnly)reader[4]
                        });
                    }
                }
            }
        }
        public List<user> GetUsers()
        {
            return users;
        }

    }
}
