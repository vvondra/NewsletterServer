using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace NewsletterServer.TransferAgent
{

    /// <summary>
    /// Takes a fixed amount of messages from all queues and sends them 
    /// </summary>
    internal class RoundRobinMessenger : Messenger
    {
        /// <summary>
        /// Limits the number of messages sent per hour for throttling
        /// </summary>
        readonly static int MessagesPerHour = 2000;

        /// <summary>
        /// How many messages should be sent during one round-robin circle
        /// </summary>
        readonly static int MessagesPerBatch = 20;

        /// <summary>
        /// MTA for sending messages
        /// </summary>
        internal MessageTransferAgent Mta { get; set; }

        /// <summary>
        /// Used for throttling, stores the last time of the cycle
        /// </summary>
        protected DateTime LastCycle = DateTime.Now;

        /// <summary>
        /// Amount of time throttling is behing schedule
        /// </summary>
        protected TimeSpan TimeDebt = new TimeSpan(0);
        /// <summary>
        /// Prepares the messenger
        /// </summary>
        /// <param name="mta">mail transfer agent to be used</param>
        internal RoundRobinMessenger(MessageTransferAgent mta)
        {
            Mta = mta;
        }

        /// <summary>
        /// Loads currently waiting messages
        /// </summary>
        /// <returns></returns>
        List<Message> LoadMessages()
        {
            using (var newsletterEntities = new NewsletterEntities()) {
                var msgQuery = from m in newsletterEntities.Messages
                               where m.status == Message.StatusWaiting
                               select m;

                var messages = new List<Message>();

                foreach (var msg in msgQuery) {
                    messages.Add(new Message(msg, newsletterEntities));
                }

                return messages;
            }
        } 

        /// <summary>
        /// Delivers selected messages
        /// </summary>
        /// <param name="messages">List of messages to be sent</param>
        internal override void Deliver()
        {
            var messages = LoadMessages();
            // Deliver messages until there are none left
            while (messages.Count > 0) {

                Throttle();
                
                foreach (var msg in messages) {

                    // Take a fair slice of subscribers so all messages get the same
                    var recepients = msg.GetUndeliveredSubscribers(MessagesPerBatch / messages.Count).ToList();

                    // No recepients left
                    if (recepients.Count() == 0) {
                        // Remove message from queue
                        messages.Remove(msg);

                        // Call delivered event
                        msg.MessageDelivered(this, null);
                        continue;
                    }

                    DeliverMessageToSubscribers(msg, recepients);
                    
                }

                // Refresh queue
                messages = LoadMessages();
            }
        }

        /// <summary>
        /// Throttles the sending loop to meet the message per hour quota
        /// 
        /// Called once each batch, it calculates the time spent on the cycle and determines
        /// how long it needs to sleep or pay a time debt to previous long cycles
        /// </summary>
        protected void Throttle()
        {
            var previousCycle = LastCycle;
            
            TimeSpan loopTime = DateTime.Now - previousCycle;

            if (loopTime.CompareTo(CycleLength) > 0) {
                // We are behing of schedule, add how much
                TimeDebt += loopTime - CycleLength;
                return;
            } else {

                TimeSpan sleepTime = CycleLength - loopTime;

                // We are ahead of schedule during one loop
                if (TimeDebt.Seconds > 0) {
                    // Cover time debt
                    var diff = sleepTime.CompareTo(TimeDebt) < 0 ? new TimeSpan(0) : sleepTime - TimeDebt; // Math.Max(0, sleepTime - TimeDebt);
                    TimeDebt -= sleepTime;
                    sleepTime = diff;
                }

                // Sleep for what is left of sleep time after covering debt
                System.Threading.Thread.Sleep(sleepTime);
            }

            LastCycle = DateTime.Now;
        }

        /// <summary>
        /// Gets the expected length of one sending cycle to maintain average message per hour quota
        /// </summary>
        protected TimeSpan CycleLength
        {
             get { return new TimeSpan(0, 0, 3600 / (MessagesPerHour / MessagesPerBatch)); }
        }

        /// <summary>
        /// Send message to specified subscribers
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="subscribers">List of recepients</param>
        protected void DeliverMessageToSubscribers(Message msg, List<Subscriber> subscribers)
        {
            foreach (var recepient in subscribers) {
                Mta.Send(recepient.contact, msg.Content);
            }
        }
    }
}