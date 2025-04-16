using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for register.xaml
    /// This page handles user registration. It allows users to input their details and create a new account.
    /// </summary>
    public partial class register : Page
    {
        // Connection string to connect to the SQL Server database
        public static string connectionString = "Data Source=YAKSH;Initial Catalog=C#project;Persist Security Info=True;User ID=sa;Password=Yaksh@2004;Encrypt=True;Trust Server Certificate=True";

        // SqlConnection object initialized with the connection string
        Sqlconnection conn = new Sqlconnection(connectionString);

        public register()
        {
            InitializeComponent();  // Initializes components defined in the XAML file
        }

        /// <summary>
        /// Navigates to the login page when the login button is clicked.
        /// </summary>
        private void login_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new loginPage()); // Navigate to login page
        }

        // This method is commented out and unused for now
        //private void lognin_Click(object sender, RoutedEventArgs e)
        //{
        //}

        /// <summary>
        /// Handles the registration logic when the user clicks the "Register" button.
        /// It validates the input and inserts the new user's details into the database.
        /// </summary>
        private void register_click(object sender, RoutedEventArgs e)
        {
            // Get the values entered by the user
            string username = usernamereg.Text.Trim();  // Username entered by the user
            string password = pass1.Password;  // Password entered by the user
            string confirmPassword = pass2.Password;  // Confirm password entered by the user
            string description = des.Text.Trim();  // User description entered by the user

            // Check if any required field is empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill all required fields.");  // Show an error message if any field is empty
                return;
            }

            // Check if the passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");  // Show an error message if passwords don't match
                return;
            }

            // Check password strength: It must contain at least one uppercase letter and one number
            if (!password.Any(char.IsUpper) || !password.Any(char.IsDigit))
            {
                MessageBox.Show("Password must contain at least one uppercase letter and one number.");  // Show error if password is weak
                return;
            }

            // Check if the username already exists in the database
            string query = "SELECT COUNT(*) FROM [user] WHERE user_name = @username";

            SqlConnection connection = conn.Getconn();  // Get a new database connection
            SqlCommand cmd = new SqlCommand(query, connection);  // Create a SQL command to check for existing username
            cmd.Parameters.AddWithValue("@username", username);  // Add the username parameter to the query
            int exists = (int)cmd.ExecuteScalar();  // Execute the query and get the count of records with the entered username

            if (exists > 0)
            {
                MessageBox.Show("Username already exists. Please choose another.");  // Show an error if the username already exists
                return;
            }

            // Insert the new user's details into the database using a stored procedure
            string insertQuery = "EXEC addnewuser @username, @user_pass, @userDes";
            SqlCommand insertCmd = new SqlCommand(insertQuery, connection);  // Create the insert command
            insertCmd.Parameters.AddWithValue("@username", username);  // Add parameters for username
            insertCmd.Parameters.AddWithValue("@user_pass", password);  // Add parameters for password
            insertCmd.Parameters.AddWithValue("@userDes", description);  // Add parameters for description
            insertCmd.ExecuteNonQuery();  // Execute the insert query to add the new user

            MessageBox.Show("Registration successful!");  // Show a success message
            this.NavigationService.Navigate(new loginPage());  // Navigate to the login page after successful registration
        }
    }
}
