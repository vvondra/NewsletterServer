using System;
using System.Web;
using System.Runtime.Serialization;

namespace NewsletterServer.DataTransferObject
{
    /// <summary>
    /// Data Transfer Object for subscribers, hides entity code
    /// </summary>
    [DataContract]
    public class MessageDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int WaitingToBeSent { get; set; }
    }
}