using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryServer.TransferAgent
{

    /// <summary>
    /// General message provider
    /// 
    /// Possible sources: database, web service, etc.
    /// </summary>
    internal abstract class MessageProvider
    {

        /// <summary>
        /// Gets a list of messages waiting to be sent
        /// </summary>
        /// <returns>list of messages to be sent</returns>
        internal abstract List<Message> GetUndeliveredMessages();
    }
}