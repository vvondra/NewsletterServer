using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsletterServer.TransferAgent
{

    /// <summary>
    /// A message to be sent, contains message body and tracks sent recepients
    /// </summary>
    internal class Message
    {

        /// <summary>
        /// Flag that the message is waiting to be sent
        /// </summary>
        public const int StatusWaiting = 0;

        /// <summary>
        /// Flag that the message was delivered to all recepients
        /// </summary>
        public const int StatusDelivered = 3;

        /// <summary>
        /// Entity context
        /// </summary>
        NewsletterEntities objectContext { get; set; }

        /// <summary>
        /// Content of the message to be sent
        /// </summary>
        public NewsletterServer.Message Content;

        /// <summary>
        /// Handler called when the message is set as delivered
        /// </summary>
        internal EventHandler MessageDelivered;

        /// <summary>
        /// Creates a message container
        /// </summary>
        /// <param name="msg">message data</param>
        public Message(NewsletterServer.Message msg, NewsletterEntities context)
        {
            Content = msg;
            objectContext = context;
            MessageDelivered += this.OnDelivery;
        }

        /// <summary>
        /// Returns list of subscribers 
        /// </summary>
        /// <returns></returns>
        public List<Subscriber> GetUndeliveredSubscribers(int count)
        {
            var subscribersQuery = from s in Content.Subscribers
                                   select s;
            return subscribersQuery.Take(count).ToList();
        }

        /// <summary>
        /// Set the message delivered to the specified subscriber
        /// </summary>
        /// <param name="s">subscriber message was sent to</param>
        public void AddDelivery(Subscriber s)
        {
            var queueQuery = from q in Content.Subscribers
                             where q.contact == s.contact
                             select q;

            // Delete subscriber from queue for this mailing
            objectContext.DeleteObject(queueQuery.First());
        }

        /// <summary>
        /// Sets the message status as delivered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnDelivery(object sender, EventArgs args)
        {
            Content.status = StatusDelivered;
            objectContext.SaveChanges();
        }

    }
}