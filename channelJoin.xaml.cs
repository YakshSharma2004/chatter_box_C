using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for channelJoin.xaml
    /// This page allows the user to join available channels.
    /// </summary>
    public partial class channelJoin : Page
    {
        private readonly SqlConnection sqlConnection;  // Connection to the database
        private string userid;  // ID of the current user
        private string username;  // Username of the current user
        private ObservableCollection<channel> channelList = new ObservableCollection<channel>();  // List to hold available channels

        public channelJoin()
        {
            InitializeComponent();  // Initializes the components defined in the XAML file
        }

        /// <summary>
        /// Constructor that initializes the page with the necessary parameters (SQL connection, user ID, username).
        /// </summary>
        public channelJoin(SqlConnection sqlconnnection, string userid, string username)
        {
            InitializeComponent();  // Initializes the components
            this.sqlConnection = sqlconnnection;  // Sets the connection object
            this.userid = userid;  // Sets the user ID
            this.username = username;  // Sets the username
        }

        /// <summary>
        /// Called when the page is loaded. Fetches all available channels that the current user has not yet joined.
        /// </summary>
        public void channelJoin_loaded(object sender, RoutedEventArgs e)
        {
            getAllChannels();  // Calls the method to fetch channels
        }

        /// <summary>
        /// Fetches all channels the user has not yet joined from the database.
        /// </summary>
        public void getAllChannels()
        {
            string query = "SELECT cha_id, channel_name, channel_description FROM dbo.dis_channel WHERE cha_id NOT IN (SELECT dis_channel_chan_id FROM dis_channel_user WHERE dis_channel_user_id = @userid);";

            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@userid", userid);  // Adds user ID as a parameter to the query
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())  // Reads each channel record
                    {
                        // Adds each channel to the ObservableCollection for dynamic UI updates
                        channelList.Add(new channel
                        {
                            id = int.Parse(reader["cha_id"].ToString()),  // Channel ID
                            name = reader["channel_name"].ToString(),  // Channel Name
                            description = reader["channel_description"].ToString()  // Channel Description
                        });
                    }
                }
            }
            channel_list.ItemsSource = channelList;  // Sets the ObservableCollection as the ItemsSource for the ListBox
        }

        /// <summary>
        /// Handles the search button click event to filter channels based on the search text.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            channelList.Clear();  // Clears the existing list of channels
            channel_list.ItemsSource = null;  // Clears the current list view
            string query = "SELECT cha_id, channel_name,channel_description FROM dbo.dis_channel where channel_name like @encha;";

            // SQL query to search for channels based on the entered text
            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@encha", "%" + search_txt.Text + "%");  // Adds the search query parameter
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())  // Reads the filtered channels
                    {
                        channelList.Add(new channel
                        {
                            id = int.Parse(reader["cha_id"].ToString()),  // Channel ID
                            name = reader["channel_name"].ToString(),  // Channel Name
                            description = reader["channel_description"].ToString()  // Channel Description
                        });
                    }
                }
            }
            channel_list.ItemsSource = channelList;  // Updates the ListBox with the filtered channels
        }

        /// <summary>
        /// Handles the join button click event. Adds the user to the selected channel.
        /// </summary>
        public void join_btn_Click(object sender, RoutedEventArgs e)
        {
            if (channel_list.SelectedItem != null)  // Checks if a channel is selected
            {
                channel selectedChannel = (channel)channel_list.SelectedItem;  // Gets the selected channel
                string channelName = selectedChannel.name;  // Channel Name
                string channelId = selectedChannel.id.ToString();  // Channel ID

                // Adds the user to the selected channel in the database
                string query = "insert into dis_channel_user(dis_channel_chan_id, dis_channel_user_id) values(@chanelId, @userid);";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@chanelId", channelId);  // Channel ID
                    cmd.Parameters.AddWithValue("@userid", userid);  // User ID
                    if (cmd.ExecuteNonQuery() == 1)  // Executes the insert query
                    {
                        MessageBox.Show("You have joined the channel: " + channelName);  // Success message
                    }
                    else
                    {
                        MessageBox.Show("Failed to join the channel.");  // Failure message
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a channel to join.");  // Prompt if no channel is selected
            }
        }

        /// <summary>
        /// Handles selection changes in the ListBox (currently not used).
        /// </summary>
        private void channel_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Code for handling selection change could be added here if needed
        }

        /// <summary>
        /// Navigates back to the main landing page when the back button is clicked.
        /// </summary>
        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new mainLanding(username, sqlConnection));  // Navigate back to the main landing page
        }
    }
}
