using System;
using System.Windows;
using System.ServiceModel;
using MahApps.Metro.Controls;

namespace ClientSide
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        internal event EventHandler LoginSuccessful;

        public LoginWindow() { 
            InitializeComponent(); 
        }

        private void logInButton_Click(object sender, RoutedEventArgs e) {
            var key = GetAuthenticationKey(username.Text, password.Text);
            
            if (key != String.Empty) {
                MessageBox.Show(String.Format("Key is {0}", key));
                LoginSuccessful(this, null);
                Close();
            } else {
                MessageBox.Show("Invalid credentials");
            }
        }

        string GetAuthenticationKey(string username, string password)
        {
            var client = new AuthServiceClient();
            return client.GetAuthKey(username, password);
        }
    }
}
