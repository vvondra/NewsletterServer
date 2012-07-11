﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewsletterServer
{

    /// <summary>
    /// Endpoint for subscriber related features
    /// </summary>
    [ServiceContract]
    interface ISubscriberService
    {
        [OperationContract]
        DataTransferObject.Subscriber[] GetSubscribers(int value, string authKey);

        /// <summary>
        /// Queues message in the sending queue common for all newsletters
        /// </summary>
        /// <param name="subject">Subject of the message</param>
        /// <param name="body">Content of the message (with HTML or other markup)</param>
        /// <param name="clean_body">Content of the message without any markup</param>
        /// <returns>Return true when queueing was successful</returns>
        [OperationContract]
        bool QueueMessage(int subject, int body, int clean_body);
    }
}
