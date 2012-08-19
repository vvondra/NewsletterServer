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
        List<DataTransferObject.SubscriberDto> GetSubscribers(string authKey);


        /// <summary>
        /// Adds the sent subscriber to the database
        /// </summary>
        /// <param name="authKey"></param>
        /// <param name="subscriber">subscriber to be saved</param>
        [OperationContract]
        void AddSubscriber(string authKey, DataTransferObject.SubscriberDto subscriber);

        /// <summary>
        /// Updates a subscriber in the database
        /// </summary>
        /// <param name="authKey"></param>
        /// <param name="subscriber">new subscriber data, matched by ID</param>
        [OperationContract]
        void UpdateSubscriber(string authKey, DataTransferObject.SubscriberDto subscriber);

        /// <summary>
        /// Deletes a subscriber from newsletter
        /// </summary>
        /// <param name="authKey"></param>
        /// <param name="id">id of the user</param>
        [OperationContract]
        void DeleteSubscriber(string authKey, int id);
    }
}
