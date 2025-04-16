using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for friends.xaml
    /// This page displays the user's list of friends and allows interactions such as removing friends.
    /// </summary>
    public partial class friends : Page
    {
        private string username;  // Username of the current user
        private SqlConnection connection;  // SQL connection to the database
        public string userid;  // User ID of the current user

        // ObservableCollection to hold the list of friends (used for dynamic UI updates)
        public ObservableCollection<string> friends_list = new ObservableCollection<string>();

        public friends(string username, SqlConnection sqlconnnection, string userid)
        {
            InitializeComponent();  // Initializes components defined in the XAML file
            this.username = username;  // Sets the current user's username
            connection = sqlconnnection;  // Sets the SQL connection
            this.userid = userid;  // Sets the current user's ID
            friends_list.Clear();  // Clears the existing friends list
        }

        /// <summary>
        /// Called when the page is loaded. Sets up the list view and loads the friends data.
        /// </summary>
        public void friends_loaded(object sender, RoutedEventArgs e)
        {
            names.ItemsSource = null;  // Clears the existing items source
            names.Items.Clear();  // Clears the ListBox items
            names.ItemsSource = friends_list;  // Binds the ObservableCollection to the ListBox
            getFriends();  // Loads the list of friends from the database
        }

        /// <summary>
        /// Fetches the current user's friends from the database and populates the friends_list.
        /// </summary>
        private void getFriends()
        {
            string query = "EXEC GetUserFriends @userid;";  // Stored procedure to get friends for the current user
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@userid", this.userid);  // Passes the user ID as a parameter
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Adds each friend's name to the ObservableCollection
                    while (reader.Read())
                    {
                        friends_list.Add(reader[1].ToString());  // Adds friend's name (second column)
                    }
                }
            }
        }

        private string friendname;  // Stores the selected friend's name

        /// <summary>
        /// Handles selection change in the ListBox of friends. Displays selected friend's details and adds a "Remove Friend" button.
        /// </summary>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (names.SelectedItem == null)  // If no item is selected, return
            {
                return;
            }

            string friendName = names.SelectedItem.ToString();  // Gets the selected friend's name
            string userdataq = "select user_name, UserDescription, DateOfJoin from dbo.[user] where user_name = @friendname;";  // Query to get friend's details
            using (SqlCommand cmd = new SqlCommand(userdataq, connection))
            {
                cmd.Parameters.AddWithValue("@friendname", friendName);  // Passes friend's name as parameter
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())  // If data is found for the friend
                    {
                        // Get friend's details from the database
                        string userName = reader[0].ToString();
                        string userDescription = reader[1].ToString();
                        DateTime dateOfJoin;

                        try
                        {
                            dateOfJoin = reader.GetDateTime(2);  // Get the friend's join date
                        }
                        catch (Exception)
                        {
                            // Handle case where the date is null or not in the expected format
                            dateOfJoin = DateTime.MinValue;  // Set default date value
                        }

                        // Update the UI with the friend's details
                        friendNameText.Text = "User Name: " + userName;
                        friendDescriptionText.Text = "Description: " + userDescription;
                        friendDateOfJoinText.Text = "Date of Join: " + dateOfJoin.ToLongDateString();
                    }
                }
            }

            // Remove any existing "Remove Friend" button
            Button existingButton = friendDetailsPanel.Children
                                    .OfType<Button>()
                                    .FirstOrDefault(btn => btn.Name == "removeFriendButton");
            if (existingButton != null)
                friendDetailsPanel.Children.Remove(existingButton);

            // Create and add a new "Remove Friend" button
            Button removeButton = new Button
            {
                Content = "Remove Friend",
                Name = "removeFriendButton",
                Margin = new Thickness(0, 20, 0, 0),
                Width = 120,
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = Brushes.IndianRed,
                Foreground = Brushes.White
            };

            // Button click event to remove friend
            removeButton.Click += (s, ev) => RemoveFriend(friendName);
            friendDetailsPanel.Children.Add(removeButton);
        }

        /// <summary>
        /// Fetches the user ID of the selected friend from the database.
        /// </summary>
        private string friendUserid()
        {
            friendname = names.SelectedItem.ToString();  // Gets the selected friend's name
            string idquery = "SELECT user_id FROM dbo.[user] WHERE user_name=@friendName;";  // Query to get friend's user ID
            using (SqlCommand idcmd = new SqlCommand(idquery, connection))
            {
                idcmd.Parameters.AddWithValue("@friendName", friendname);  // Passes the friend's name as parameter
                using (SqlDataReader reader = idcmd.ExecuteReader())
                {
                    if (reader.Read())  // If data is found for the friend
                    {
                        return reader[0].ToString();  // Return the friend's user ID
                    }
                    else
                        return null;  // Return null if no data found
                }
            }
        }

        /// <summary>
        /// Removes a friend from the user's friend list in the database and UI.
        /// </summary>
        public void RemoveFriend(string friendName)
        {
            int frienduserid = int.Parse(friendUserid());  // Get the friend's user ID
            string removeFriendQuery = "EXEC remove_friend @userid, @frienduserid;";  // Stored procedure to remove friend
            using (SqlCommand cmd = new SqlCommand(removeFriendQuery, connection))
            {
                cmd.Parameters.AddWithValue("@userid", this.userid);  // Passes the current user's ID as parameter
                cmd.Parameters.AddWithValue("@frienduserid", frienduserid);  // Passes the friend's user ID as parameter
                cmd.ExecuteNonQuery();  // Executes the removal query
            }

            // Remove the friend from the ObservableCollection (UI updates automatically)
            friends_list.Remove(friendName);
            names.ItemsSource = null;  // Clear the ListBox items source
            names.ItemsSource = friends_list;  // Rebind the updated friends list to the ListBox
            MessageBox.Show("Friend removed successfully.");  // Show a success message

            // Clear the friend's details UI
            Button existingButton = friendDetailsPanel.Children
                                    .OfType<Button>()
                                    .FirstOrDefault(btn => btn.Name == "removeFriendButton");
            friendNameText.Text = "";
            friendDescriptionText.Text = "";
            friendDateOfJoinText.Text = "";
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        /// <summary>
        /// Navigates back to the main landing page when the back button is clicked.
        /// </summary>
        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new mainLanding(username, connection));  // Navigate to the main landing page
        }

        /// <summary>
        /// Navigates to the friend search page when the "Add Friend" button is clicked.
        /// </summary>
        private void addFriend_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new friendSearch(connection, userid, username));  // Navigate to the friend search page
        }
    }
}
