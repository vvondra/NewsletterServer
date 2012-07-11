using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;

namespace ClientSide
{
    public class SubcriberViewModel
    {
        public ICollectionView Subscribers { get; private set; }

        public SubcriberViewModel()
        {
            var _Subscribers = new ObservableCollection<Subscriber>
                                 {
                                     new Subscriber
                                         {
                                             Name = "Vojta Vondra",
                                             Contact = "vojtavondra@gmail.com",
                                             Subscribed = true
                                         }
                                 };

            Subscribers = CollectionViewSource.GetDefaultView(_Subscribers);

        }
    }
}
