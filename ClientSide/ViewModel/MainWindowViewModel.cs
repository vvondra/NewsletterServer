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

        ReadOnlyCollection<CommandViewModel> _commands;
        readonly SubscriberService _SubscriberService;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel()
        {
            base.DisplayName = Resources.MainWindowViewModel_DisplayName;

            _SubscriberService = new SubscriberService();

            ShowLoginDialog();
        }

        #endregion // Constructor

        #region Commands

        /// <summary>
        /// Returns a read-only list of commands 
        /// that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null) {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                /*new CommandViewModel(
                    Resources.MainWindowViewModel_Command_CreateNewSubscriber,
                    new RelayCommand(param => this.CreateNewSubscriber()))*/
            };
        }

        #endregion // Commands

        /// <summary>
        /// Holds the currently active view in window
        /// </summary>
        public ViewModelBase CurrentView { get; set; }


        void CreateNewSubscriber()
        {
            Subscriber newSubscriber = Subscriber.CreateNewSubscriber();
            SubscriberViewModel workspace = new SubscriberViewModel(newSubscriber, _SubscriberService);
            this.SetActiveView(workspace);
        }

        /// <summary>
        /// Sets the login dialog as the current view
        /// </summary>
        void ShowLoginDialog()
        {
            LoginViewModel loginVm = new LoginViewModel();
            loginVm.LoginSuccessful += this.GetAuthenticationKey;
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
            _SubscriberService.AuthKey = AuthKey;
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