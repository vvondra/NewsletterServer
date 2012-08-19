using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientSide.ViewModel.Workspace
{
    public class SubscriberAddedEventArgs : EventArgs
    {
        public SubscriberAddedEventArgs(ClientSide.Model.Subscriber subscriber)
        {
            NewSubscriber = subscriber;
        }

        public ClientSide.Model.Subscriber NewSubscriber { get; private set; }
    }
}
