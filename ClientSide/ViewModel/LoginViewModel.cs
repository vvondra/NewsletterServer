using System;
using System.Security;
using System.Windows;
using System.Windows.Input;
using System.ServiceModel;
using System.ComponentModel;
using MahApps.Metro.Controls;
using ClientSide.Model;

namespace ClientSide.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// Called upon successful login
        /// </summary>
        internal event EventHandler LoginSuccessful;

        ICommand _loginCommand;

        /// <summary>
        /// Credential container
        /// </summary>
        readonly Credentials _credentials;

        bool _isProcessingLogin = false;

        #region Fields
        public string Username
        {
            get { return _credentials.Username; }
            set
            {
                if (value == _credentials.Username)
                    return;

                _credentials.Username = value;

                base.OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return _credentials.Password; }
            set
            {
                if (value == _credentials.Password)
                    return;

                _credentials.Password = value;

                base.OnPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Determines wheter the viewmodel is currenctly processing the login credentials
        /// </summary>
        public bool IsProcessingLogin
        {
            get { return _isProcessingLogin; }
            set
            {
                _isProcessingLogin = value;
                base.OnPropertyChanged("IsProcessingLogin");
            }
        }

        #endregion Fields

        public LoginViewModel()
        {
            _credentials = new Credentials();

            base.DisplayName = "Newsletter Composer - Log In";
           
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null) {
                    _loginCommand = new RelayCommand(ExecuteLogin);
                }

                return _loginCommand;
            }
        }

        /// <summary>
        /// Calls the authentication service in the background to not block the UI thread
        /// </summary>
        /// <param name="o"></param>
        private void ExecuteLogin(object o)
        {
            // We are now waiting for the login to be processed
            IsProcessingLogin = true;

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += new DoWorkEventHandler(GetAuthenticationKey);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessLogin);

            worker.RunWorkerAsync(_credentials);
        }
     
        /// <summary>
        /// Upon receiving a response from the authentication service, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProcessLogin(object sender, RunWorkerCompletedEventArgs e)
        {
            // The login is now processed and the rest is fast
            IsProcessingLogin = false;

            var key = e.Result as string;

            if (key != String.Empty) {
                LoginSuccessful(this, new LoginEventArgs(key));
            } else {
                MessageBox.Show("Invalid login information");
            }
        }

        /// <summary>
        /// Users authentication service to get auth key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void GetAuthenticationKey(object sender, DoWorkEventArgs args)
        {
            var credentials = args.Argument as Credentials;

            try {
                var client = new AuthServiceClient();
                args.Result = client.GetAuthKey(credentials.Username, credentials.Password);
            } catch (EndpointNotFoundException e) {
                args.Result = String.Empty;
            }
        }
    }
}
