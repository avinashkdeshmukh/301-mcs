using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusMessaging.Model
{
    public class TopicConfiguration
    {
        public string Name { get; set; }
        public SubscriptionConfiguration Subscription { get; set; }
    }
}
