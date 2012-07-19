using System;
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

        /// <summary>
        /// Returns an array of subscribers for newsletter logged in with current auth key
        /// </summary>
        /// <param name="value"></param>
        /// <param name="authKey">Authentication key provide by authentication serivce <see cref="IAuthService.GetAuthKey"/></param>
        /// <returns></returns>
        [OperationContract]
        DataTransferObject.SubscriberDto[] GetSubscribers(string authKey);
    }
}
