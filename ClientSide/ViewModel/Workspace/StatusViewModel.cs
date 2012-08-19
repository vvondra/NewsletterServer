using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ClientSide.Properties;
using ClientSide.Model;
using Smith.WPF.HtmlEditor;

namespace ClientSide.ViewModel.Workspace
{
    class StatusViewModel : ViewModelBase
    {

        public StatusViewModel(MessageService service)
        {
            if (service == null) {
                throw new ArgumentException("Need working message service");
            }

            _msgService = service;
            DisplayName = Resources.Status_DisplayName;
        }

        #region Private fields

        MessageService _msgService;
        ObservableCollection<Message> _messages;
        RelayCommand _refreshCommand;

        #endregion Private fields

        #region Presentation properties

        public ObservableCollection<Message> Messages
        {
            get
            {
                if (_messages == null) {
                    LoadMessages();
                }

                return _messages;
            }
            set
            {
                _messages = value;
                base.OnPropertyChanged("Messages");
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null) {
                    _refreshCommand = new RelayCommand(
                        param => this.LoadMessages(),
                        param => true
                        );
                }
                return _refreshCommand;
            }
        }


        #endregion Presentation properties

        public void LoadMessages()
        {
            Messages = new ObservableCollection<Message>(_msgService.GetMessageList());
        }

        public void OnMessageSent(object sender, MessageSentEventArgs args)
        {
            LoadMessages();
        }

    }
}
