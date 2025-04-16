using System.Windows;
using System.Windows.Controls;

namespace sql_practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This is the main window that hosts the application and handles navigation to the login page.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  // Initializes the components defined in the XAML file
        }

        /// <summary>
        /// Handles the Loaded event of the window. This event is triggered when the window is fully loaded.
        /// It navigates to the login page.
        /// </summary>
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Navigates the frame to the login page when the window is loaded
            frame.NavigationService.Navigate(new loginPage());
        }

        /// <summary>
        /// Handles the Navigated event of the frame. This event is triggered whenever navigation within the frame occurs.
        /// It is currently empty, but can be used to track navigation events if needed in the future.
        /// </summary>
        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // This method can be used for handling any actions after navigation in the frame
        }
    }
}
