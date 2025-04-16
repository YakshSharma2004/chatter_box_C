using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for requests.xaml
    /// This page displays friend requests and allows users to accept or reject them.
    /// </summary>
    public partial class requests : Page
    {
        // Constructor when no parameters are passed
        public requests()
        {
            InitializeComponent();
        }

        private SqlConnection connection;  // Database connection
        private string currentUserId;  // Current user's ID
        private string currentUserName;  // Current user's name
        private ObservableCollection<dynamic> requestData = new ObservableCollection<dynamic>();  // Observable collection for friend requests

        /// <summary>
        /// Constructor with parameters to initialize the page with database connection, user ID, and user name.
        /// </summary>
        public requests(SqlConnection connection, string user_id, string username)
        {
            InitializeComponent();
            this.connection = connection;
            this.currentUserId = user_id;
            this.currentUserName = username;
            LoadRequests();  // Loads the friend requests when the page is initialized
        }

        /// <summary>
        /// Loads the list of friend requests from the database for the current user.
        /// </summary>
        private void LoadRequests()
        {
            string query = "exec get_friend_reqs @currentUserId";  // Stored procedure to get friend requests for the current user

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@currentUserId", currentUserId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = new
                    {
                        user_id = reader["user_id"].ToString(),
                        user_name = reader["user_name"].ToString()
                    };
                    requestData.Add(item);  // Adds the request to the ObservableCollection
                }
            }

            requestList.ItemsSource = requestData;  // Bind the request data to the ListBox
        }

        /// <summary>
        /// Handles accepting a friend request.
        /// </summary>
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            dynamic user = (sender as Button)?.DataContext;  // Gets the user data from the button's DataContext
            string requesterId = user?.user_id;
            string requesterName = user?.user_name;

            if (!string.IsNullOrEmpty(requesterId))
            {
                // Update the status of the friend request to 'accepted'
                string query = @"UPDATE dbo.friend SET is_accepted = 1 WHERE (user_1_id = @requesterId AND user_2_id = @currentUserId)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@requesterId", requesterId);
                    cmd.Parameters.AddWithValue("@currentUserId", currentUserId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"✅ You accepted {requesterName}'s friend request.", "Friend Request", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Remove the accepted request from the list
            requestData.Remove(user);
        }

        /// <summary>
        /// Handles rejecting a friend request.
        /// </summary>
        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            dynamic user = (sender as Button)?.DataContext;
            string requesterId = user?.user_id;
            string requesterName = user?.user_name;

            if (!string.IsNullOrEmpty(requesterId))
            {
                // Delete the friend request from the database
                string query = @"DELETE FROM dbo.friend WHERE (user_1_id = @requesterId AND user_2_id = @currentUserId) OR (user_2_id = @requesterId AND user_1_id = @currentUserId)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@requesterId", requesterId);
                    cmd.Parameters.AddWithValue("@currentUserId", currentUserId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"❌ You rejected {requesterName}'s friend request.", "Friend Request", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Remove the rejected request from the list
            requestData.Remove(user);
        }

        /// <summary>
        /// Navigates back to the friend search page.
        /// </summary>
        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new friendSearch(this.connection, this.currentUserId, this.currentUserName));
        }
    }
}
