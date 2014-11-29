using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroServicesHackathon.Facts
{
    public class ChatFact : Fact
    {
        public override string TopicName { get { return "chat"; } }

        public string who { get; set; }
        public string says { get; set; }
    }
}
