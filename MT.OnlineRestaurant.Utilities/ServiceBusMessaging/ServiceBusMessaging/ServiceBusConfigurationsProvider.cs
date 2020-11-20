using ServiceBusMessaging.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusMessaging
{
    public class ServiceBusConfigurationsProvider
    {
        public string ConnectionString { get; set; }
        public List<TopicConfiguration> Topics { get; set; }

        public ServiceBusConfiguration GetConfiguration(string topicName)
        {
            var config = new ServiceBusConfiguration();
            config.ConnectionString = ConnectionString;
            config.Topic = Topics.Find(tc => string.Equals(tc.Name, topicName, StringComparison.InvariantCultureIgnoreCase));
            return config;
        }
    }
}
