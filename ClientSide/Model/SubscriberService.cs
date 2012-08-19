using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsletterServer.DataTransferObject;
using ClientSide.ViewModel.Workspace;

namespace ClientSide.Model
{
    public class SubscriberService : IDisposable
    {

        /// <summary>
        /// Client used to connect to web service
        /// </summary>
        SubscriberServiceClient client = new SubscriberServiceClient();

        /// <summary>
        /// List of subscribers
        /// </summary>
        List<Subscriber> _subscribers;

        /// <summary>
        /// Raised when a subscriber is added
        /// </summary>
        public event EventHandler<SubscriberChangedEventArgs> SubscriberAdded;

        /// <summary>
        /// Raised when a subscriber is deleted
        /// </summary>
        public event EventHandler<SubscriberChangedEventArgs> SubscriberDeleted;

        /// <summary>
        /// Auth key for service client
        /// </summary>
        public string AuthKey { get; set; }

        public SubscriberService(string key)
        {
            if (key == String.Empty) {
                throw new ArgumentException("Service needs valid authentication key");
            }

            AuthKey = key;
        }

        /// <summary>
        /// Returns all subscribers
        /// If not previously loaded, loads them from the repository
        /// </summary>
        /// <returns></returns>
        public List<Subscriber> GetSubscribers()
        {
            if (_subscribers != null) {
                return _subscribers;
            }

            var subscribers = client.GetSubscribers(AuthKey);

            _subscribers = new List<Subscriber>();
            foreach (var subscriber in subscribers) {
                _subscribers.Add(Subscriber.CreateSubscriber(subscriber.Id, subscriber.Name, subscriber.IsSubscribed, subscriber.Contact));
            }

            return _subscribers;
        }

        /// <summary>
        /// Adds a subscriber to the collection and sends and add request to the service
        /// </summary>
        /// <param name="s">subscriber to be added</param>
        public void SaveSubscriber(Subscriber s)
        {
            if (!ContainsSubscriber(s)) {
                // Add to collection
                _subscribers.Add(s);

                // Send to service
                var dto = new NewsletterServer.DataTransferObject.SubscriberDto();
                dto.Contact = s.Email;
                dto.Name = s.Name;
                dto.IsSubscribed = true;

                // Add user and fetch new Id
                s.Id = client.AddSubscriber(AuthKey, dto);

                // Raise added event
                if (SubscriberAdded != null) {
                    SubscriberAdded(this, new SubscriberChangedEventArgs(s));
                }
            } else {
                var dto = new NewsletterServer.DataTransferObject.SubscriberDto();
                dto.Contact = s.Email;
                dto.Name = s.Name;

            }
        }

        /// <summary>
        /// Sends a deletion request for the subscriber
        /// </summary>
        /// <param name="s">subscriber to be removed</param>
        public void DeleteSubscriber(Subscriber s)
        {
            if (ContainsSubscriber(s)) {
                client.DeleteSubscriber(AuthKey, s.Id);
            }

            if (SubscriberDeleted != null) {
                SubscriberDeleted(this, new SubscriberChangedEventArgs(s));
            }
        }

        /// <summary>
        /// Checks whether the current subscriber list contains a subscriber
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool ContainsSubscriber(Subscriber s)
        {
            return GetSubscribers().Contains(s);
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
