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
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Fields

        /// <summary>
        /// Authentication key received after login
        /// </summary>
        public string AuthKey { get; set; }

        ReadOnlyCollection<CommandViewModel> _commands;
        readonly SubscriberService _SubscriberService;
        ObservableCollection<WorkspaceViewModel> _views;

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

        #region Workspaces

        /// <summary>
        /// Returns the collection of available views
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Views
        {
            get
            {
                if (_views == null) {
                    _views = new ObservableCollection<WorkspaceViewModel>();
                    _views.CollectionChanged += this.OnViewsChanged;
                }
                return _views;
            }
        }



        void OnViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnViewRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnViewRequestClose;
        }

        void OnViewRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            this.Views.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers

        void CreateNewSubscriber()
        {
            Subscriber newSubscriber = Subscriber.CreateNewSubscriber();
            SubscriberViewModel workspace = new SubscriberViewModel(newSubscriber, _SubscriberService);
            this.Views.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ShowLoginDialog()
        {
            LoginViewModel loginVm = new LoginViewModel();
            loginVm.LoginSuccessful += this.GetAuthenticationKey;
            this.Views.Add(loginVm);
            this.SetActiveWorkspace(loginVm);
        }

        void GetAuthenticationKey(object sender, EventArgs args)
        {
            AuthKey = (args as LoginEventArgs).AuthKey;
            _SubscriberService.AuthKey = AuthKey;
        }

        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Views.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Views);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion // Private Helpers
    }
}