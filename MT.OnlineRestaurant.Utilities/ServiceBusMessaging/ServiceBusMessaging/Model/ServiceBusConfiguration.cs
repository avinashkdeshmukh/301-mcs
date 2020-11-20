using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusMessaging.Model
{
    public class ServiceBusConfiguration
    {
        public string ConnectionString { get; set; }
        public TopicConfiguration Topic { get; set; }
    }
}
