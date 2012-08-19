using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsletterServer.DataTransferObject;
using ClientSide.ViewModel.Workspace;

namespace ClientSide.Model
{
    public class MessageService : IDisposable
    {

        /// <summary>
        /// Called when a message is sent
        /// </summary>
        public EventHandler<MessageSentEventArgs> MessageSent;

        /// <summary>
        /// Creates a service through which messages can be sent to server
        /// </summary>
        /// <param name="key">service authentication key</param>
        public MessageService(string key)
        {
            client = new MessageServiceClient();
            if (key == String.Empty) {
                throw new ArgumentException("Service needs valid authentication key");
            }

            authKey = key;
        }

        #region private fields
        /// <summary>
        /// Message client
        /// </summary>
        MessageServiceClient client;

        /// <summary>
        /// Service authentication key
        /// </summary>
        string authKey;
        #endregion

        /// <summary>
        /// Queues a message in the server
        /// </summary>
        /// <param name="subject">message subject</param>
        /// <param name="body">message body</param>
        /// <param name="clean_body">message body without HTML</param>
        public void QueueMessage(string subject, string body, string clean_body)
        {
            client.QueueMessage(subject, body, clean_body, authKey);
            if (MessageSent != null) {
                MessageSent(this, new MessageSentEventArgs());
            }
        }

        /// <summary>
        /// Returns a list of messages from the server
        /// </summary>
        /// <returns>list of messages for authenticated newsletter</returns>
        public List<Message> GetMessageList()
        {
            var msgs = client.GetMessageList(authKey);
            var msgList = new List<Message>();

            foreach (var msg in msgs) {
                var msgItem = new Message();
                msgItem.Id = msg.Id;
                msgItem.Text = msg.Text;
                msgItem.Date = msg.Date;
                msgItem.Status = msg.Status;
                msgItem.Subject = msg.Subject;
                msgItem.WaitingToBeSent = msg.WaitingToBeSent;
                msgList.Add(msgItem);
            }

            return msgList;
        }

        /// <summary>
        /// Close connection on disposal
        /// </summary>
        public void Dispose()
        {
            client.Close();
        }
    }
}
