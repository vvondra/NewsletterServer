using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;
using System.Threading;
using NewsletterServer.User;
using System.Web.Services.Protocols;

namespace NewsletterServer
{

    /// <summary>
    /// Service providing all communication with newsletter service
    /// Maintains a delivery thread in the background to send queued messages
    /// </summary>
    public class NewsletterService : IAuthService, ISubscriberService, IMessageService
    {

        /// <summary>
        /// Holds authenticated sessions
        /// </summary>
        private SessionManager sessions = new SessionManager();

        /// <summary>
        /// Creates mappings from ADO entities to Data Transfer Objects
        /// Hides internal entity objects
        /// </summary>
        static void CreateMappings()
        {
            AutoMapper.Mapper.CreateMap<Subscriber, DataTransferObject.SubscriberDto>();
            AutoMapper.Mapper.CreateMap<Message, DataTransferObject.MessageDto>();
        }

        /// <inheritdoc />
        public string GetAuthKey(string username, string password)
        {
            try {
                using (var context = new NewsletterEntities()) {
                    var newsletter = new ObjectParameter("ret", typeof(Int32));
                    context.GetUserNewsletter(username, password, newsletter);
                    var newsletterId = Int32.Parse(newsletter.Value.ToString());
                    if (newsletterId > 0) {
                        return sessions.CreateSession(username, newsletterId);
                    }

                    return String.Empty;
                }
            } catch (Exception e) {
                return String.Empty;
            }
        }

        /// <inheritdoc />
        public DataTransferObject.SubscriberDto[] GetSubscribers(string authKey)
        {
            if (!IsAuthenticatedKey(authKey)) {
                return null;
            }

            // Fetch all subscribers for authed user
            using (var context = new NewsletterEntities()) {
                var subscriberQuery = from s in context.Subscribers
                                      where s.newsletter == sessions.GetSession(authKey).NewsletterId
                                      select s;

                var subscribers = new List<DataTransferObject.SubscriberDto>();
                CreateMappings();
                foreach (var subscriber in subscriberQuery) {
                    subscribers.Add(AutoMapper.Mapper.Map<Subscriber, DataTransferObject.SubscriberDto>(subscriber));
                }

                return subscribers.ToArray();
            }
        }

        /// <inheritdoc />
        public bool QueueMessage(string subject, string body, string clean_body, string authKey)
        {
            if (!IsAuthenticatedKey(authKey)) {
                return false;
            }

            // Create a message entity and store it
            using (var context = new NewsletterEntities()) {
                var message = new Message();
                message.status = 3; // TODO DeliveryServer.TransferAgent.Message.StatusWaiting;
                message.text = body;
                message.clean_text = clean_body;
                message.subject = subject;
                message.newsletter = sessions.GetSession(authKey).NewsletterId;

                context.Messages.AddObject(message);

                // Persist changes
                context.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Checks whether the user is correctly authenticated
        /// </summary>
        /// <param name="authKey">authentication key</param>
        /// <returns>true when authenticated</returns>
        bool IsAuthenticatedKey(string authKey)
        {
            // Check authentication
            if (!sessions.IsAuthenticated(authKey)) {
                return false;
            }

            // Revalidate user
            sessions.BumpSession(authKey);

            return true;
        }

    }
}
