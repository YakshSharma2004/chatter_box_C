using Microsoft.Data.SqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for friendInfo.xaml
    /// This page displays information about a selected friend and allows sending a friend request.
    /// </summary>
    public partial class friendInfo : Page
    {
        private SqlConnection connection;  // SQL connection to the database
        private string user_id;  // Current user's ID
        private string username;  // Current user's username
        private string user_selected;  // The selected friend's username
        private int user_id_friend;  // The selected friend's user ID

        public friendInfo()
        {
            InitializeComponent();  // Initializes the components defined in the XAML file
        }

        public friendInfo(string username, SqlConnection connection, string user_id, string user_selected)
        {
            InitializeComponent();  // Initializes the components
            this.username = username;  // Sets the current user's username
            this.user_id = user_id;  // Sets the current user's ID
            this.connection = connection;  // Sets the SQL connection
            this.user_selected = user_selected;  // Sets the selected friend's username
        }

        /// <summary>
        /// Called when the page is loaded. Can be used to initialize data or refresh the page.
        /// </summary>
        public void friendInfo_Loaded(object sender, RoutedEventArgs e)
        {
            //getUserInfo();  // This method can be called to fetch user's information (currently commented out)
        }

        /// <summary>
        /// Fetches the selected friend's details (username, description, date of join) from the database.
        /// </summary>
        public void getUserInfo()
        {
            string query = "SELECT user_name, UserDescription, DateOfJoin, user_id FROM dbo.[user] WHERE user_name = @user_name";  // SQL query to get friend's information
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@user_name", user_selected);  // Passes the selected friend's username as parameter
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())  // If data is found for the selected friend
                    {
                        string username = reader["user_name"].ToString();  // Gets the friend's username
                        string description = reader["UserDescription"].ToString();  // Gets the friend's description
                        DateTime dateOfJoin = reader.GetDateTime(2);  // Gets the date of join
                        user_id_friend = Convert.ToInt32(reader["user_id"]);  // Gets the friend's user ID

                        // Updates the UI with the friend's information
                        user_name.Text = username;
                        user_des.Text = description;
                        join_date.Text = dateOfJoin.ToLongDateString();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the click event for the back button. Navigates to the friend search page.
        /// </summary>
        public void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new friendSearch(connection, user_id, username));  // Navigates to the friend search page
        }

        /// <summary>
        /// Handles the click event for the "Send Friend Request" button. Sends a friend request to the selected friend.
        /// </summary>
        private void request_btn_Click(object sender, RoutedEventArgs e)
        {
            string query = "EXEC friend_req_sent @user_id, @friend_user_id";  // Stored procedure to send a friend request
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@user_id", user_id);  // Passes the current user's ID
                cmd.Parameters.AddWithValue("@friend_user_id", user_id_friend);  // Passes the selected friend's ID

                try
                {
                    // Executes the query to send the friend request
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Friend request sent successfully.");  // Success message
                    }
                }
                catch (Microsoft.Data.SqlClient.SqlException)
                {
                    // Error handling for when the friend request has already been sent
                    MessageBox.Show("Friend request already sent.");
                }
            }
        }

        /// <summary>
        /// Called when the page is loaded. This method is used to load the user's information when the page is shown.
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            getUserInfo();  // Fetch and display the selected friend's information when the page loads
        }
    }
}
