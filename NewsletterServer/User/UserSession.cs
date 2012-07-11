using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsletterServer.User
{

    /// <summary>
    /// Container for user session
    /// </summary>
    public class UserSession
    {
        public string Username { get; set; }

        public int NewsletterId { get; set; }

        public string AuthenticationKey { get; set; }

        public DateTime TimeAuthenticated { get; set; }
    }
}