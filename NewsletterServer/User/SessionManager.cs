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
        /// Entity object context
        /// </summary>
        NewsletterEntities context;
        
        /// <summary>
        /// How long a session lasts without refreshing (default two hours)
        /// </summary>
        public TimeSpan SessionTimeout = new TimeSpan(2, 0, 0);

        public SessionManager(NewsletterEntities context)
        {
            this.context = context;
        }

        /// <summary>
        /// Creates a user session object and registers it in the manager
        /// </summary>
        /// <param name="username">username</param>
        /// <return>generated authentication key</return>
        internal string CreateSession(string username)
        {
            var authKey = GenerateAuthKey();

            var userIdQuery = from u in context.Users
                              where u.username == username
                              select u.id;

            int user_id = userIdQuery.FirstOrDefault();

            var userQuery = from s in context.Sessions
                            where user_id == s.user_id
                            select s;

            var session = userQuery.FirstOrDefault();
            if (session != null && IsAuthenticated(session.auth_key)) {
                BumpSession(session.auth_key);
                return session.auth_key;
            }

            context.Sessions.AddObject(Sessions.CreateSessions(user_id, authKey, DateTime.Now));
            context.SaveChanges();

            return authKey;
        }

        internal UserSession GetSession(string authKey)
        {
            var sessionQuery = from s in context.Sessions
                               join u in context.Users on s.user_id equals u.id
                               select new UserSession() { Username = u.username, NewsletterId = u.newsletter, AuthenticationKey = s.auth_key, TimeAuthenticated = s.time };
            return sessionQuery.FirstOrDefault();
        }

        /// <summary>
        /// Removes sessions older than set timeout
        /// </summary>
        internal void PruneSessions()
        {
            var timeOutDate = DateTime.Now.Subtract(SessionTimeout);
            var oldSessionQuery = from s in context.Sessions
                                  where s.time < timeOutDate
                                  select s;

            foreach (var session in oldSessionQuery) {
                context.DeleteObject(session);
            }

            context.SaveChanges();

        }

        /// <summary>
        /// Checks whether the user has an active session
        /// </summary>
        /// <param name="authKey">authentication key user has been authenticated with</param>
        /// <returns></returns>
        internal bool IsAuthenticated(string authKey)
        {
            PruneSessions();
            return GetSession(authKey) != null;
        }

        /// <summary>
        /// Bumps the specified session last modified time
        /// Does nothing if session does not exist
        /// </summary>
        /// <param name="authKey">authentication key user has been authenticated with</param>
        internal void BumpSession(string authKey) {
            var sessionQuery = from s in context.Sessions
                               where s.auth_key == authKey
                               select s;
            var session = sessionQuery.FirstOrDefault();
            session.time = DateTime.Now;
            context.SaveChanges();
        }

        /// <summary>
        /// Generates a unique authentication key
        /// </summary>
        /// <returns>authentication key</returns>
        string GenerateAuthKey()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            return GuidString;
        }

    }
}