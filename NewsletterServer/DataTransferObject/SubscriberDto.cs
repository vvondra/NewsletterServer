using System;
using System.Web;
using System.Runtime.Serialization;

namespace NewsletterServer.DataTransferObject
{
    /// <summary>
    /// Data Transfer Object for subscribers, hides entity code
    /// </summary>
    [DataContract]
    [Serializable]
    public class SubscriberDto
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Contact { get; set; }

        [DataMember]
        public bool IsSubscribed { get; set; }
    }
}