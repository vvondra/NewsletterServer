using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientSide.ViewModel
{
    /// <summary>
    /// Used to hold arguments received upon authentication
    /// </summary>
    class LoginEventArgs : EventArgs
    {
        public string AuthKey { get; set; }

        public LoginEventArgs(string authKey)
        {
            AuthKey = authKey;
        }
    }
}
