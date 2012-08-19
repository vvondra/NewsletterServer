using System;
using System.Collections.Generic;
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
    class ComposeViewModel : ViewModelBase
    {

        public ComposeViewModel(MessageService service)
        {
            if (service == null) {
                throw new ArgumentException("Need working message service");
            }

            _msgService = service;
            DisplayName = Resources.Compose_DisplayName;
            Editor = new HtmlEditor();
        }

        #region Private fields

        string _subject;
        HtmlEditor _editor;
        RelayCommand _sendCommand;
        MessageService _msgService;

        #endregion Private fields


        #region Presentation properites

        /// <summary>
        /// Message subject
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                base.OnPropertyChanged("Subject");
            }
        }

        /// <summary>
        /// HTML editor
        /// </summary>
        public HtmlEditor Editor
        {
            get { return _editor; }
            set { 
                _editor = value;
                base.OnPropertyChanged("Editor"); 
            }
        }

        #endregion Presentation properties

        /// <summary>
        /// Returns a command that sends the message
        /// </summary>
        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null) {
                    _sendCommand = new RelayCommand(
                        param => this.Send(),
                        param => this.CanSend
                        );
                }
                return _sendCommand;
            }
        }

        void Send()
        {
            _msgService.QueueMessage(Subject, Editor.ContentHtml, Editor.ContentText);
        }

        /// <summary>
        /// Checks whether the message can be sent
        /// Checks for an empty message are implemented
        /// </summary>
        /// <returns>true when message is filled out and can be sent</returns>
        public bool CanSend
        {
            get
            {
                return Subject.Trim() != String.Empty && Editor != null && Editor.WordCount > 0;
            }
        }
    }
}
