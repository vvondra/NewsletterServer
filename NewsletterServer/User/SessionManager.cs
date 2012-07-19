using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsletterServer.User
{
    /// <summary>
    /// Manages active session with the service
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// List of active sessions
        /// </summary>
        Dictionary<string, UserSession> Sessions = new Dictionary<string, UserSession>();
        
        /// <summary>
        /// How long a session lasts without refreshing (default two hours)
        /// </summary>
        public TimeSpan SessionTimeout = new TimeSpan(2, 0, 0);
        
        /// <summary>
        /// Creates a user session object and registers it in the manager
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="authKey">generated authentication key</param>
        /// <param name="newsletterId">ID of users newsletter</param>
        internal void CreateSession(string username, string authKey, int newsletterId)
        {
            var session = new UserSession{
                Username = username,
                AuthenticationKey = authKey,
                NewsletterId = newsletterId,
                TimeAuthenticated = DateTime.Now
            };

            Sessions.Add(authKey, session);
        }

        internal UserSession GetSession(string authKey)
        {
            return Sessions[authKey];
        }

        /// <summary>
        /// Removes sessions older than set timeout
        /// </summary>
        internal void PruneSessions()
        {
            foreach (KeyValuePair<string, UserSession> kvp in Sessions) {
                if (DateTime.Now.Subtract(SessionTimeout).CompareTo(kvp.Value.TimeAuthenticated) > 0) {
                    Sessions.Remove(kvp.Key);
                }
            }
        }

        /// <summary>
        /// Checks whether the user has an active session
        /// </summary>
        /// <param name="authKey">authentication key user has been authenticated with</param>
        /// <returns></returns>
        internal bool IsAuthenticated(string authKey)
        {
            PruneSessions();
            return Sessions.ContainsKey(authKey);
        }

        /// <summary>
        /// Bumps the specified session last modified time
        /// Does nothing if session does not exist
        /// </summary>
        /// <param name="authKey">authentication key user has been authenticated with</param>
        internal void BumpSession(string authKey) {
            if (Sessions.ContainsKey(authKey)) {
                Sessions[authKey].TimeAuthenticated = DateTime.Now;
            }
        }
    }
}