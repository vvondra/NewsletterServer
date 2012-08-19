using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ClientSide.Properties;
using ClientSide.Model;

namespace ClientSide.ViewModel.Workspace
{
    public class SubscriberListViewModel : ViewModelBase
    {

        public SubscriberListViewModel(SubscriberService service)
        {
            DisplayName = Resources.SubscriberList_DisplayName;

            if (service == null) {
                throw new ArgumentNullException("Invalid service");
            }

            _subscriberService = service;

            _subscriberService.SubscriberAdded += this.OnSubscriberAddedToRepository;
            _subscriberService.SubscriberDeleted += this.OnSubscriberDeletedFromRepository;

            LoadSubscribers();
        }

        #region Fields

        readonly SubscriberService _subscriberService;

        ViewModelBase _subscriberPane;

        #endregion // Fields


        void LoadSubscribers()
        {
            var subs = _subscriberService.GetSubscribers();
            List<SubscriberViewModel> all =
                (from cust in subs
                 select new SubscriberViewModel(cust, _subscriberService)).ToList();

            foreach (SubscriberViewModel cvm in all)
                cvm.PropertyChanged += this.OnSubscriberViewModelPropertyChanged;

            this.AllSubscribers = new ObservableCollection<SubscriberViewModel>(all);
            this.AllSubscribers.CollectionChanged += this.OnCollectionChanged;
        }



        /// <summary>
        /// Returns a collection of all the SubscriberViewModel objects.
        /// </summary>
        public ObservableCollection<SubscriberViewModel> AllSubscribers { get; private set; }

        /// <summary>
        /// Holds the viewmodel for the right hand side subscriber editing pane
        /// </summary>
        public ViewModelBase SubscriberPane
        {
            get
            {
                if (_subscriberPane == null) {
                    _subscriberPane = new SubscriberViewModel(Subscriber.CreateNewSubscriber(), _subscriberService);
                }
                return _subscriberPane;
            }
            set
            {
                _subscriberPane = value;
                base.OnPropertyChanged("SubscriberPane");
            }
        }


        protected override void OnDispose()
        {
            foreach (SubscriberViewModel custVM in this.AllSubscribers)
                custVM.Dispose();

            this.AllSubscribers.Clear();
            this.AllSubscribers.CollectionChanged -= this.OnCollectionChanged;

            _subscriberService.SubscriberAdded -= this.OnSubscriberAddedToRepository;
        }


        #region Event Handling Methods

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (SubscriberViewModel svm in e.NewItems)
                    svm.PropertyChanged += this.OnSubscriberViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (SubscriberViewModel svm in e.OldItems)
                    svm.PropertyChanged -= this.OnSubscriberViewModelPropertyChanged;
        }

        void OnSubscriberViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // When a subscriber row is selected, set the user form to the selected user
            if (sender is SubscriberViewModel) {
                SubscriberPane = sender as SubscriberViewModel;
            }

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as SubscriberViewModel).VerifyPropertyName(IsSelected);
        }

        void OnSubscriberAddedToRepository(object sender, SubscriberChangedEventArgs e)
        {
            var viewModel = new SubscriberViewModel(e.Subscriber, _subscriberService);
            this.AllSubscribers.Add(viewModel);
        }

        void OnSubscriberDeletedFromRepository(object sender, SubscriberChangedEventArgs e)
        {
            SubscriberPane = new SubscriberViewModel(Subscriber.CreateNewSubscriber(), _subscriberService);
            for (int i = 0; i < this.AllSubscribers.Count; i++) {
                if (this.AllSubscribers[i].Subscriber == e.Subscriber) {
                    this.AllSubscribers.RemoveAt(i);
                }
            }
        }

        #endregion // Event Handling Methods
    }
}
