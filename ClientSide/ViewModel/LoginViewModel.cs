using System;
using System.Security;
using System.Windows;
using System.Windows.Input;
using System.ServiceModel;
using MahApps.Metro.Controls;
using ClientSide.Models;

namespace ClientSide.ViewModel
{
    public class LoginViewModel : WorkspaceViewModel
    {

        internal event EventHandler LoginSuccessful;

        ICommand _loginCommand;

        readonly Credentials _credentials;

        #region Credential fields
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
        #endregion Credentialfields

        public LoginViewModel()
        {
            _credentials = new Credentials();
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

        private void ExecuteLogin(object o)
        {
            var key = GetAuthenticationKey(_credentials.Username, _credentials.Password);
            
            if (key != String.Empty) {
                MessageBox.Show(String.Format("Key is {0}", key));
                LoginSuccessful(this, new LoginEventArgs(key));
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
