using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for create_new_chnl.xaml
    /// </summary>
    public partial class create_new_chnl : Page
    {
        private string username;  // Username of the current user
        private SqlConnection connection;  // SQL connection to the database    
        private string user_id;  // User ID of the current user
        public create_new_chnl()
        {
            InitializeComponent();
        }
        public create_new_chnl(string userid, SqlConnection conn)
        {
            InitializeComponent();
            this.username = username;
            connection = conn;
            this.user_id = userid;  // Sets the user ID
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string channelName = chl_name.Text;  // Get the channel name from the TextBox
            string channelDescription = chl_des.Text;  // Get the channel description from the TextBox
            string createChannelQuery = "Insert into dis_channel (channel_name,channel_admin,channel_description) values(@ch_name,@ch_ad,@ch_des);";  // SQL query to create a new channel
            using (SqlCommand cmd = new SqlCommand(createChannelQuery, connection))
            {
                cmd.Parameters.AddWithValue("@ch_name", channelName);  // Passes the channel name as parameter
                cmd.Parameters.AddWithValue("@ch_ad", user_id);  // Passes the channel admin (user ID) as parameter
                cmd.Parameters.AddWithValue("@ch_des", channelDescription);  // Passes the channel description as parameter
                try
                {
                    cmd.ExecuteNonQuery();  // Execute the insert command
                    MessageBox.Show("Channel created successfully!");  // Show success message
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating channel: " + ex.Message);  // Show error message if any exception occurs
                }
            }
        }
    }
}
