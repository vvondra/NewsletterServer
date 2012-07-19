using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryServer.TransferAgent
{
    internal abstract class MessageTransferAgent
    {

        /// <summary>
        /// Sends specified message to an address
        /// </summary>
        /// <param name="address">destination address</param>
        /// <param name="msg">message entity</param>
        /// <returns>true on successful delivery</returns>
        internal abstract bool Send(string address, NewsletterServer.Message msg);
    }
}