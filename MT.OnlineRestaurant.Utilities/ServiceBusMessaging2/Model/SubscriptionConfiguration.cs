using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusMessaging.Model
{
    public class SubscriptionConfiguration
    {
        public string Name { get; set; }
        public int MaxConcurrentCalls { get; set; }
        public bool AutoComplete { get; set; }
    }
}
