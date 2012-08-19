using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientSide.ViewModel.Workspace
{
    public class SubscriberChangedEventArgs : EventArgs
    {
        public SubscriberChangedEventArgs(ClientSide.Model.Subscriber subscriber)
        {
            Subscriber = subscriber;
        }

        public ClientSide.Model.Subscriber Subscriber { get; private set; }
    }
}
