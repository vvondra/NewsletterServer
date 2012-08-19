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
    /// </summary>
    public class NewsletterService : IAuthService, ISubscriberService, IMessageService
    {

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
                    var sessions = new SessionManager(context);

                    var newsletter = new ObjectParameter("ret", typeof(Int32));
                    context.GetUserNewsletter(username, password, newsletter);
                    var newsletterId = Int32.Parse(newsletter.Value.ToString());
                    if (newsletterId > 0) {
                        return sessions.CreateSession(username);
                    }

                    return String.Empty;
                }
            } catch (Exception e) {
                return String.Empty;
            }
        }

        /// <inheritdoc />
        public List<DataTransferObject.SubscriberDto> GetSubscribers(string authKey)
        {
            if (!IsAuthenticatedKey(authKey)) {
                return null;
            }

            // Fetch all subscribers for authed user
            using (var context = new NewsletterEntities()) {
                var sessions = new SessionManager(context);
                var newsletterId = sessions.GetSession(authKey).NewsletterId;
                var subscriberQuery = from s in context.Subscribers
                                      where s.newsletter == newsletterId
                                      select s;

                var subscribers = new List<DataTransferObject.SubscriberDto>();
                CreateMappings();
                foreach (var subscriber in subscriberQuery) {
                    var mapped = AutoMapper.Mapper.Map<Subscriber, DataTransferObject.SubscriberDto>(subscriber);
                    subscribers.Add(mapped);
                }

                return subscribers;
            }
        }

        /// <inheritdoc />
        public int AddSubscriber(string authKey, DataTransferObject.SubscriberDto s)
        {
            if (!IsAuthenticatedKey(authKey)) {
                return 0;
            }

            using (var context = new NewsletterEntities()) {
                var subscriber = new Subscriber();
                var sessions = new SessionManager(context);
                subscriber.name = s.Name;
                subscriber.contact = s.Contact;
                subscriber.newsletter = sessions.GetSession(authKey).NewsletterId;

                context.Subscribers.AddObject(subscriber);
                context.SaveChanges();

                return subscriber.id;
            }
        }

        /// <inheritdoc />
        public void UpdateSubscriber(string authKey, DataTransferObject.SubscriberDto sub)
        {
            if (!IsAuthenticatedKey(authKey)) {
                return;
            }

            using (var context = new NewsletterEntities()) {
                var sessions = new SessionManager(context);
                var newsletterId = sessions.GetSession(authKey).NewsletterId;
                var subscriberQuery = from s in context.Subscribers
                                      where s.newsletter == newsletterId && s.id == sub.Id
                                      select s;

                foreach (var row in subscriberQuery) {
                    row.name = sub.Name;
                    row.contact = sub.Contact;
                }

                context.SaveChanges();
            }
        }

        /// <inheritdoc />
        public void DeleteSubscriber(string authKey, int id)
        {
            if (!IsAuthenticatedKey(authKey)) {
                return;
            }

            // Create a message entity and store it
            using (var context = new NewsletterEntities()) {

                var sessions = new SessionManager(context);
                var newsletterId = sessions.GetSession(authKey).NewsletterId;
                var subscriberQuery = from s in context.Subscribers
                                      where s.newsletter == newsletterId && s.id == id
                                      select s;

                foreach (var row in subscriberQuery) {
                    context.Subscribers.DeleteObject(row);
                }

                // Persist changes
                context.SaveChanges();
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

                var sessions = new SessionManager(context);
                var message = new Message();
                message.status = 3; // TODO DeliveryServer.TransferAgent.Message.StatusWaiting;
                message.text = body;
                message.clean_text = clean_body;
                message.subject = subject;
                message.newsletter = sessions.GetSession(authKey).NewsletterId;
                message.date = DateTime.Now;

                context.Messages.AddObject(message);

                // Persist changes
                context.SaveChanges();
            }

            return true;
        }

        /// <inheritdoc />
        public List<NewsletterServer.DataTransferObject.MessageDto> GetMessageList(string authKey)
        {
            if (!IsAuthenticatedKey(authKey)) {
                return null;
            }

            // Fetch all subscribers for authed user
            using (var context = new NewsletterEntities()) {
                var sessions = new SessionManager(context);
                var newsletterId = sessions.GetSession(authKey).NewsletterId;
                var msgQuery = from m in context.Messages
                                      where m.newsletter == newsletterId
                                      select m;

                var msgs = new List<DataTransferObject.MessageDto>();
                CreateMappings();
                foreach (var msg in msgQuery) {
                    var mapped = AutoMapper.Mapper.Map<Message, DataTransferObject.MessageDto>(msg);
                    switch (msg.status) {
                        case 0:
                            mapped.Status = "Done";
                            break;
                        case 1:
                            mapped.Status = "Canceled";
                            break;
                        case 3:
                            mapped.Status = "Queued";
                            break;
                    }

                    var queueCountQuery = from s in msg.Subscribers select s;
                    mapped.WaitingToBeSent = queueCountQuery.Count();

                    msgs.Add(mapped);
                }

                return msgs;
            }
        }

        /// <summary>
        /// Checks whether the user is correctly authenticated
        /// </summary>
        /// <param name="authKey">authentication key</param>
        /// <returns>true when authenticated</returns>
        bool IsAuthenticatedKey(string authKey)
        {
            var sessions = new SessionManager(new NewsletterEntities());
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
