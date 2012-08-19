using System;
using System.ComponentModel;
using System.Windows.Input;
using ClientSide.Model;
using ClientSide.Properties;
using NewsletterServer;

namespace ClientSide.ViewModel.Workspace
{
    /// <summary>
    /// A UI-friendly wrapper for a Subscriber object.
    /// </summary>
    public class SubscriberViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields

        readonly Subscriber _subscriber;
        readonly SubscriberService _subscriberService;
        bool _isSelected;
        RelayCommand _saveCommand;

        #endregion // Fields

        #region Constructor

        public SubscriberViewModel(Subscriber subscriber, SubscriberService subscriberService)
        {
            if (subscriber == null)
                throw new ArgumentNullException("subscriber");

            if (subscriberService == null)
                throw new ArgumentNullException("subscriberService");

            _subscriber = subscriber;
            _subscriberService = subscriberService;
        }

        #endregion // Constructor

        #region Customer Properties

        public string Email
        {
            get { return _subscriber.Email; }
            set
            {
                if (value == _subscriber.Email)
                    return;

                _subscriber.Email = value;

                base.OnPropertyChanged("Email");
            }
        }

        public string Name
        {
            get { return _subscriber.Name; }
            set
            {
                if (value == _subscriber.Name)
                    return;

                _subscriber.Name = value;

                base.OnPropertyChanged("Name");
            }
        }

        public bool IsSubscribed
        {
            get { return _subscriber.IsSubscribed; }
        }


        #endregion // Customer Properties

        #region Presentation Properties

        public override string DisplayName
        {
            get
            {
                if (IsNewSubsciber) {
                    return "register new subscriber";
                }
                return "edit subscriber " + _subscriber.Name;
            }
            protected set
            {
                
            }
        }

        /// <summary>
        /// Text for the save button below subscriber form
        /// </summary>
        public string SaveButtonText
        {
            get {
                if (IsNewSubsciber) {
                    return "Add";
                }
                return "Save";
            }
        }

        /// <summary>
        /// Gets/sets whether this customer is selected in the UI.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;

                _isSelected = value;

                base.OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Returns a command that saves the customer.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null) {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => this.CanSave
                        );
                }
                return _saveCommand;
            }
        }

        #endregion // Presentation Properties

        #region Public Methods

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {
            if (!_subscriber.IsValid)
                throw new InvalidOperationException(Resources.SubscriberViewModel_Exception_CannotSave);

            if (this.IsNewSubsciber)
                _subscriberService.SaveSubscriber(_subscriber);

            base.OnPropertyChanged("DisplayName");
        }

        #endregion // Public Methods

        #region Private Helpers

        /// <summary>
        /// Returns true if this subscriber was created by the user and it has not yet
        /// been saved to the subscriber repository.
        /// </summary>
        bool IsNewSubsciber
        {
            get { return !_subscriberService.ContainsSubscriber(_subscriber); }
        }

        /// <summary>
        /// Returns true if the subscriber is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get { return _subscriber.IsValid; }
        }

        #endregion // Private Helpers

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get { return (_subscriber as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                error = (_subscriber as IDataErrorInfo)[propertyName];

                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }


        #endregion // IDataErrorInfo Members
    }
}