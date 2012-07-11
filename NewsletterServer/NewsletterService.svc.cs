using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;

namespace NewsletterServer
{

    /// <summary>
    /// Service providing all communication with newsletter service
    /// </summary>
    public class NewsletterService : IAuthService, ISubscriberService, IMessageService
    {

        Dictionary<string, int> Users = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            CreateMappings();
        }

        /// <summary>
        /// Creates mappings from ADO entities to Data Transfer Objects
        /// </summary>
        static void CreateMappings()
        {
            AutoMapper.Mapper.CreateMap<Subscriber, DataTransferObject.Subscriber>();
        }

        bool IsValidKey(string authKey)
        {
            return true;
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
                        Users.Add(key, newsletterId);
                        
                        return key;
                    }

                    return String.Empty;
                }
            } catch (Exception e) {
                return String.Empty;
            }
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

        /// <inheritdoc />
        public DataTransferObject.Subscriber[] GetSubscribers(string authKey)
        {
            if (IsValidKey(authKey)) {

            }

            return null;
        }

        /// <inheritdoc />
        public bool QueueMessage(int subject, int body, int clean_body, string authKey)
        {
            throw new NotImplementedException();
        }
    }
}
