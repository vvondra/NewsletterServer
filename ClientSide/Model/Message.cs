using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientSide.Model
{
    public class Message
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public int WaitingToBeSent { get; set; }
    }
}
