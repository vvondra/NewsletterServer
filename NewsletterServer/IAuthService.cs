using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewsletterServer
{
    [ServiceContract]
    public interface IAuthService
    {
        /// <summary>
        /// Returns auth key for subsequent requests for authorization in other newsletter services
        /// On unsuccessful login, return an empty string
        /// </summary>
        /// <param name="username">username to authenticate with</param>
        /// <param name="password">password to authenticate with</param>
        /// <returns>string with authentication key</returns>
        [OperationContract]
        string GetAuthKey(string username, string password);

    }
}
