using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;

namespace NewsletterServer.TransferAgent
{

    /// <summary>
    /// Message provider implementation using Entity Framework
    /// </summary>
    internal class EntityMessageProvider : MessageProvider
    {

        /// <summary>
        /// Object context to access database
        /// </summary>
        NewsletterEntities context;


        /// <summary>
        /// Creates new database message provider
        /// </summary>
        /// <param name="objectContext">object context for entities</param>
        internal EntityMessageProvider(NewsletterEntities objectContext)
        {
            context = objectContext;
        }

        /// <summary>
        /// Loads message from the database
        /// </summary>
        /// <returns>list of messages waiting to be sent</returns>
        internal override List<Message> GetUndeliveredMessages()
        {
            
            var msgQuery = from m in context.Messages
                            where m.status == Message.StatusWaiting
                            select m;

            var messages = new List<Message>();

            foreach (var msg in msgQuery) {
                messages.Add(new Message(msg, context));
            }

            return messages;
        }

    }
}