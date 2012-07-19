using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DeliveryServer
{
    /// <summary>
    /// Maintains a delivery thread in the background to send queued messages
    /// </summary>
    class Server
    {
        static void Main(string[] args)
        {
            // Start sending in background
            var deliveryThread = SpawnDeliveryAgent(
                new TransferAgent.SmtpTransferAgent(),
                new TransferAgent.EntityMessageProvider(new NewsletterServer.NewsletterEntities())
            );

            deliveryThread.Join();
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
    }
}
