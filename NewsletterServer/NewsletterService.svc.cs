using System;
using System.Data.Objects;
using System.Threading;
using NewsletterServer.User;

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
        /// Sets up application, spawns delivery thread
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CreateMappings();

            // Start sending in background
            var deliveryThread = SpawnDeliveryAgent(
                new TransferAgent.SmtpTransferAgent(),
                new TransferAgent.EntityMessageProvider(new NewsletterEntities())
            );
        }

        /// <summary>
        /// Creates a thread that runs the delivey process
        /// </summary>
        /// <param name="mta">message transer agent to take care of sending</param>
        /// <param name="provider">message provider</param>
        /// <returns>spawned thread</returns>
        static Thread SpawnDeliveryAgent(TransferAgent.MessageTransferAgent mta, TransferAgent.MessageProvider provider)
        {
            var messenger = new TransferAgent.RoundRobinMessenger(mta, provider);
            var t = new Thread(() => messenger.Deliver());
            t.Start();

            // We do not want the delivery thread to stop the termination of the application
            t.IsBackground = true;

            return t;
        }

        /// <summary>
        /// Creates mappings from ADO entities to Data Transfer Objects
        /// </summary>
        static void CreateMappings()
        {
            AutoMapper.Mapper.CreateMap<Subscriber, DataTransferObject.Subscriber>();
            AutoMapper.Mapper.CreateMap<Message, DataTransferObject.Message>();
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
                        string key = GenerateAuthKey();
                        sessions.CreateSession(username, key, newsletterId);
                        return key;
                    }

                    return String.Empty;
                }
            } catch (Exception e) {
                return String.Empty;
            }
        }

        /// <inheritdoc />
        public DataTransferObject.Subscriber[] GetSubscribers(string authKey)
        {
            return null;
        }

        /// <inheritdoc />
        public bool QueueMessage(int subject, int body, int clean_body, string authKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a unique authentication key
        /// </summary>
        /// <returns>authentication key</returns>
        string GenerateAuthKey()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }
    }
}
