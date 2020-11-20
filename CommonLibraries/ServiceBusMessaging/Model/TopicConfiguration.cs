using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusMessaging.Model
{
    public class TopicConfiguration
    {
        public string Name { get; set; }
        public string ServiceNamespace { get; set; }
        public string ConnectionString { get; set; }
    }
}
