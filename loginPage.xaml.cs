using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for loginPage.xaml
    /// This is the login page that allows users to enter their username and password to log into the application.
    /// </summary>
    public partial class loginPage : Page
    {
        // Connection string for connecting to the SQL Server database
        public static string connectionString = "Data Source=YAKSH;Initial Catalog=C#project;Persist Security Info=True;User ID=sa;Password=Yaksh@2004;Encrypt=True;Trust Server Certificate=True";

        // SqlConnection object initialized with the connection string
        Sqlconnection conn = new Sqlconnection(connectionString);

        public loginPage()
        {
            InitializeComponent(); // Initializes the components defined in the XAML file
        }

        // Placeholder for handling text changes in the username TextBox (currently unused)
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        // Placeholder for handling text changes in the password TextBox (currently unused)
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        /// <summary>
        /// Handles the click event for the login button.
        /// Attempts to log the user in by verifying the entered username and password with the database.
        /// </summary>
        public void lognin_Click(object sender, RoutedEventArgs e)
        {
            // Create a new SQL connection
            SqlConnection newconn = conn.Getconn();

            // SQL query to fetch user credentials from the database
            string query = "select user_name, user_pass from dbo.[user];";

            // Variables to hold user-entered data and data fetched from the database
            string userd;
            string passwordd;

            // Fetch entered password and username from the UI
            string passe = pass.Password;  // password input by user
            string userenter = username.Text;  // username input by user

            bool suc = false;  // Flag to check if login was successful

            try
            {
                // Execute the SQL query to get user credentials from the database
                using (SqlCommand comm = new SqlCommand(query, newconn))
                {
                    // Read the result set returned by the query
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        // Loop through the result set (could be multiple users)
                        while (reader.Read())
                        {
                            // Fetch username and password from the reader object
                            userd = reader[0].ToString(); // First column (user_name)
                            passwordd = reader[1].ToString(); // Second column (user_pass)

                            // Check if the entered username and password match any record in the database
                            if (string.Equals(userenter, userd, StringComparison.OrdinalIgnoreCase) && passe.Equals(passwordd))
                            {
                                // If login is successful, navigate to the main landing page
                                MessageBox.Show("Login is successful!!!" + "\n" + "Welcome " + userenter);
                                this.NavigationService.Navigate(new mainLanding(userenter, newconn));
                                suc = true;
                            }
                        }

                        // If no match was found, show an error message
                        if (!suc)
                        {
                            MessageBox.Show("Entered Username Or Password was wrong!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any exceptions that occur during the database operations
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the click event when the user clicks on the "register" link to navigate to the registration page.
        /// </summary>
        private void register_click_log(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Navigate to the register page
            this.NavigationService.Navigate(new register());
        }
    }
}
