using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsletterServer.TransferAgent
{

    /// <summary>
    /// Base class for messengers
    /// 
    /// Each messenger defines its own strategy for delivery
    /// </summary>
    internal abstract class Messenger
    {

        /// <summary>
        /// Delivers provided message
        /// </summary>
        /// <param name="message">message</param>
        internal abstract void Deliver();
    }
}