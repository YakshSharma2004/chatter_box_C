using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for friendSearch.xaml
    /// This page allows the user to search for other users who are not already friends.
    /// </summary>
    public partial class friendSearch : Page
    {
        SqlConnection connection;  // Database connection object
        string user_id;  // ID of the current user
        string username;  // Username of the current user

        public ObservableCollection<string> UserList { get; set; } = new ObservableCollection<string>();  // List to store user names for display in the UI

        /// <summary>
        /// Constructor that initializes the page with required parameters and sets up the data context for binding.
        /// </summary>
        public friendSearch(SqlConnection connection, string user_id, string username)
        {
            InitializeComponent();  // Initializes the page components defined in XAML
            this.connection = connection;  // Sets the connection object for database operations
            this.user_id = user_id;  // Sets the current user's ID
            this.username = username;  // Sets the current user's username

            this.DataContext = this; // Allows binding between the UI elements and the UserList collection
        }

        /// <summary>
        /// Handles the search button click event and triggers the user search function.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getAllUsers();  // Calls method to fetch users based on the search query
        }

        /// <summary>
        /// Clears the user list when the page is loaded.
        /// </summary>
        public void friendSearch_Loaded(object sender, RoutedEventArgs e)
        {
            UserList.Clear();  // Clears any previous search results
        }

        /// <summary>
        /// Fetches the list of users that match the search query, excluding the current user and already existing friends.
        /// </summary>
        private void getAllUsers()
        {
            UserList.Clear();  // Clears the existing user list
            string query = "SELECT user_name,user_id FROM dbo.[user] WHERE user_name LIKE @searchquery AND user_name != @username AND user_id not in (select case when user_1_id=@userid then user_2_id else user_1_id  end from dbo.friend where (user_1_id = @userid OR user_2_id = @userid));";

            // SQL query to fetch users who match the search query and are not already friends
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@searchquery", "%" + search_txt.Text + "%");  // Adds search text as parameter
                cmd.Parameters.AddWithValue("@username", username);  // Excludes the current user from the search
                cmd.Parameters.AddWithValue("@userid", user_id);  // Excludes already friends from the results

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Reads the results and adds user names to the UserList collection
                    while (reader.Read())
                    {
                        UserList.Add(reader[0].ToString());  // Adds the username to the ObservableCollection
                    }
                }
            }
        }

        /// <summary>
        /// Navigates back to the friends page when the back button is clicked.
        /// </summary>
        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new friends(username, connection, user_id));  // Navigates to the friends page
        }

        /// <summary>
        /// Handles the event when a user is selected from the list, and navigates to the friend details page.
        /// </summary>
        private void user_lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string user_selected = user_lst.SelectedItem?.ToString();  // Gets the selected user's name
            // Navigates to the friendInfo page to show details of the selected user
            this.NavigationService.Navigate(new friendInfo(username, connection, user_id, user_selected));
        }

        /// <summary>
        /// Navigates to the friend requests page when the "Requests" button is clicked.
        /// </summary>
        private void reqs_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new requests(connection, user_id, username));  // Navigates to the requests page
        }
    }
}
