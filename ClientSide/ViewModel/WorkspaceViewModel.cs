using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using ClientSide.Properties;
using ClientSide.ViewModel.Workspace;

namespace ClientSide.ViewModel
{
    class WorkspaceViewModel : ViewModelBase
    {

        #region Fields

        public string AuthKey { get; set; }
        ObservableCollection<ViewModelBase> _workspaces;
        ViewModelBase _selectedItem;

        public ViewModelBase SelectedWorkspace
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                base.OnPropertyChanged("SelectedWorkspace");
            }
        }

        #endregion


        public WorkspaceViewModel(string authKey)
        {
            base.DisplayName = "Newsletter Composer";

            AuthKey = authKey;
        }


        #region Workspaces

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<ViewModelBase> Workspaces
        {
            get
            {
                if (_workspaces == null) {

                    var msgService = new ClientSide.Model.MessageService(AuthKey);
                    var svm = new StatusViewModel(msgService);

                    // Switch to status page after queing a message
                    msgService.MessageSent += delegate(object sender, MessageSentEventArgs args)
                    {
                        SelectedWorkspace = svm;
                    };

                    _workspaces = new ObservableCollection<ViewModelBase> {
                        new SubscriberListViewModel(new ClientSide.Model.SubscriberService(AuthKey)),
                        new ComposeViewModel(msgService),
                        svm,
                    };

                    SelectedWorkspace = _workspaces.First();
                }
                return _workspaces;
            }
        }

        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            System.Diagnostics.Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);

            SelectedWorkspace = workspace;
        }

        #endregion // Workspaces
    }
}
