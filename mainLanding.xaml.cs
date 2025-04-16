using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for mainLanding.xaml
    /// </summary>
    public partial class mainLanding : Page
    {
        // SQL Server connection and user info
        private SqlConnection connection;
        private string username;
        private string Userid;

        // ObservableCollection to bind chat messages to the ListBox
        ObservableCollection<string> chat = new ObservableCollection<string>();

        // Default constructor
        public mainLanding()
        {
            InitializeComponent();
            chat_list.Items.Clear(); // Clear chat list on load
        }

        // Constructor with parameters for username and connection
        public mainLanding(string username, SqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
            this.username = username;
            chat_list.Items.Clear(); // Clear chat list on load
        }

        // Called when the page is loaded
        public void mainLanding_loaded(object sender, RoutedEventArgs e)
        {
            getAllGroups();  // Load all groups user is part of
            getUserId();     // Get the user ID from the username
        }

        // (Unused) Handler for message text change
        private void message_TextChanged(object sender, TextChangedEventArgs e) { }

        // Called when a group is selected
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getAllChat(); // Load chat messages for the selected group
        }

        // Called when the Send button is clicked
        private void send_button_Click(object sender, RoutedEventArgs e)
        {
            string newchat = chat_input.Text;
            string newChatCmd = "EXEC chat_insert @newcht,@chanelid,@userId";

            // Prevent sending if no channel is selected
            if (channel_list.SelectedItem == null)
            {
                MessageBox.Show("Please select a channel to chat.");
                return;
            }

            string chanels = channel_list.SelectedItem.ToString();
            int chaid;

            // Try getting the channel ID
            if (!int.TryParse(GetChannelID(chanels), out chaid))
            {
                MessageBox.Show("Invalid channel ID");
                return;
            }

            // Insert chat message using stored procedure
            using (SqlCommand idcmd = new SqlCommand(newChatCmd, connection))
            {
                idcmd.Parameters.AddWithValue("@newcht", newchat);
                idcmd.Parameters.AddWithValue("@chanelid", chaid);
                idcmd.Parameters.AddWithValue("@userId", Userid);

                idcmd.ExecuteNonQuery(); // Execute insert
                getAllChat(); // Refresh chat
            }

            chat_input.Text = ""; // Clear input box
        }

        // Gets user ID from username
        public void getUserId()
        {
            string idquery = "SELECT user_id FROM dbo.[user] WHERE user_name=@Username;";
            using (SqlCommand idcmd = new SqlCommand(idquery, connection))
            {
                idcmd.Parameters.AddWithValue("@Username", username);
                using (SqlDataReader reader = idcmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Userid = reader[0].ToString();
                    }
                }
            }
        }

        // Gets channel ID from name
        public string GetChannelID(string chanelname)
        {
            string ret = "";
            string cmd = "SELECT cha_id FROM dis_channel WHERE channel_name =@channelName;";
            string chanels = channel_list.SelectedItem.ToString();

            using (SqlCommand idcmd = new SqlCommand(cmd, connection))
            {
                idcmd.Parameters.AddWithValue("@channelName", chanels);
                using (SqlDataReader reader = idcmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ret = reader[0].ToString(); // Return ID if found
                        return ret;
                    }
                    else { return ret; }
                }
            }
        }

        // Loads all chat messages for the selected channel
        public void getAllChat()
        {
            if (channel_list.SelectedItem == null) return; // If no group selected, return

            string chanels = channel_list.SelectedItem.ToString();
            string idquery = "SELECT user_id FROM dbo.[user] WHERE user_name=@Username;";
            string getchat = @"SELECT chat_content, chat_timestamp, user_name 
                               FROM dbo.chat 
                               JOIN dbo.[user] u ON u.user_id = chat.user_id 
                               WHERE channel_id = (SELECT cha_id FROM dis_channel WHERE channel_name = @channelName);";

            // Re-fetch user ID (just in case)
            using (SqlCommand idcmd = new SqlCommand(idquery, connection))
            {
                idcmd.Parameters.AddWithValue("@Username", username);
                using (SqlDataReader reader = idcmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Userid = reader[0].ToString();
                    }
                }
            }

            chat.Clear(); // Clear old messages

            // Fetch and format chat messages
            using (SqlCommand chatCmd = new SqlCommand(getchat, connection))
            {
                chatCmd.Parameters.AddWithValue("@channelName", chanels);
                using (SqlDataReader reader = chatCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string message = $"{reader["chat_timestamp"]}: {reader["chat_content"]} by {reader[2]}";
                        chat.Add(message);
                    }
                }
            }

            // Rebind ObservableCollection to ListBox
            chat_list.ItemsSource = null;
            chat_list.Items.Clear();
            chat_list.ItemsSource = chat;
        }

        // Load all channels/groups the user is a member of
        public void getAllGroups()
        {
            ObservableCollection<string> group = new ObservableCollection<string>();
            string chcmd = @"SELECT channel_name 
                             FROM dis_channel c 
                             JOIN dis_channel_user j ON c.cha_id = j.dis_channel_chan_id 
                             JOIN [dbo].[user] u ON u.user_id = j.dis_channel_user_id 
                             WHERE u.user_name = @UserName;";

            using (SqlCommand grpcmd = new SqlCommand(chcmd, connection))
            {
                grpcmd.Parameters.AddWithValue("@UserName", username);
                try
                {
                    using (SqlDataReader reader = grpcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            group.Add(reader[0].ToString()); // Add group name
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()); // Error if SQL fails
                }
            }

            channel_list.ItemsSource = group; // Bind group names to ListBox
        }

        // Navigate to friends page
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new friends(this.username, this.connection, this.Userid));
        }

        // Navigate to login page
        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new loginPage());
        }

        // Navigate to join channel page
        private void join_new_channel_btn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new channelJoin(this.connection, this.Userid, this.username));
        }

        private void newchl(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new create_new_chnl(Userid, connection));
        }
    }
}
