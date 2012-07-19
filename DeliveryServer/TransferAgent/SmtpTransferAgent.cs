using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace DeliveryServer.TransferAgent
{

    /// <summary>
    /// SMTP implementation of MTA
    /// </summary>
    internal class SmtpTransferAgent : MessageTransferAgent
    {

        /// <summary>
        /// Sends a message to the recepient through an SMTP server
        /// </summary>
        /// <param name="address">recepient e-mail</param>
        /// <param name="msg">message</param>
        /// <returns>true on successful delivery</returns>
        internal override bool Send(string address, NewsletterServer.Message msg)
        {
            var mail = new MailMessage("me", address, msg.subject, msg.text);
            try {
                GetClient().Send(mail);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        /// <summary>
        /// Fetches a configured SMTP client
        /// </summary>
        /// <returns>ready-to-use SMTP client</returns>
        SmtpClient GetClient()
        {
            return new SmtpClient();
        }
    }
}