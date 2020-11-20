using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusMessaging.Model
{
    public class SubscriptionConfiguration
    {
        public string Name { get; set; }
        public string ServiceNamespace { get; set; }
        public string ConnectionString { get; set; }
        public string Topic { get; set; }
        public int MaxConcurrentCalls { get; set; }
        public bool AutoComplete { get; set; }
    }
}
