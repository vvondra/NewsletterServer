using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewsletterServer
{

    /// <summary>
    /// Service providing all communication with newsletter service
    /// </summary>
    public class NewsletterService : IAuthService, ISubscriberService
    {
        static void Main(string[] args)
        {
            
        }

        /// <summary>
        /// Creates mappings from ADO entities to Data Transfer Objects
        /// </summary>
        static void CreateMappings()
        {
            AutoMapper.Mapper.CreateMap<Subscriber, DataTransferObject.Subscriber>();
        }

        /// <inheritdoc />
        public string GetAuthKey(string username, string password)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public DataTransferObject.Subscriber[] GetSubscribers(int value, string authKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool QueueMessage(int subject, int body, int clean_body)
        {
            throw new NotImplementedException();
        }
    }
}
