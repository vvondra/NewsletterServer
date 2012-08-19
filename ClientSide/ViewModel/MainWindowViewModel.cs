using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using ClientSide.Model;
using ClientSide.Properties;

namespace ClientSide.ViewModel
{
    /// <summary>
    /// The ViewModel for the application's main window.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// Authentication key received after login
        /// </summary>
        public string AuthKey { get; set; }

        /// <summary>
        /// Holds current view
        /// </summary>
        ViewModelBase _currentView;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel()
        {
            base.DisplayName = Resources.MainWindowViewModel_DisplayName;

            ShowLoginDialog();
        }

        #endregion // Constructor


        /// <summary>
        /// Holds the currently active view in window
        /// </summary>
        public ViewModelBase CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                base.OnPropertyChanged("CurrentView");
            }
        }


        void ShowWorkspace()
        {
            var workspaceVm = new WorkspaceViewModel(AuthKey);
            this.SetActiveView(workspaceVm);
        }

        /// <summary>
        /// Sets the login dialog as the current view
        /// </summary>
        void ShowLoginDialog()
        {
            LoginViewModel loginVm = new LoginViewModel();

            // Fetch authentication key from login
            loginVm.LoginSuccessful += this.GetAuthenticationKey;
            
            // When logged in, show the main workspace
            loginVm.LoginSuccessful += (s, e) => ShowWorkspace();

            this.SetActiveView(loginVm);
        }

        /// <summary>
        /// Sets the auth key for the view model from the login call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void GetAuthenticationKey(object sender, EventArgs args)
        {
            AuthKey = (args as LoginEventArgs).AuthKey;
        }

        /// <summary>
        /// Sets the view as the currently active in the window
        /// </summary>
        /// <param name="view">view to be selected as primary</param>
        void SetActiveView(ViewModelBase view)
        {
            this.CurrentView = view;
        }

    }
}