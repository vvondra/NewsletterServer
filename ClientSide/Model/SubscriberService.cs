using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientSide.Model
{
    public class SubscriberService
    {

        /// <summary>
        /// Client used to connect to web service
        /// </summary>
        SubscriberServiceClient client = new SubscriberServiceClient();

        /// <summary>
        /// List of subscribers
        /// </summary>
        List<Subscriber> _subscribers = new List<Subscriber>();

        /// <summary>
        /// Auth key for service client
        /// </summary>
        public string AuthKey { get; set; }

        public SubscriberService()
        {
           
        }

        public void LoadSubscribers()
        {
            var subscribers = client.GetSubscribers(AuthKey);
        }

        public void AddSubscriber(Subscriber s)
        {

        }

        public bool ContainsSubscriber(Subscriber s)
        {
            return false;
        }

    }
}
