using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusMessaging.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
    public class ServiceBusTopicSender
    {
        private readonly ServiceBusConfiguration _serviceBusConfiguration;
        private TopicClient _topicClient;

        public ServiceBusTopicSender(ServiceBusConfiguration serviceBusConfiguration)
        {
            this._serviceBusConfiguration = serviceBusConfiguration;
            ConfigureTopic();
        }

        public Dictionary<String, ServiceBusTopicSubscription> Subscriptions { get; set; }

        private void ConfigureTopic()
        {
            _topicClient = new TopicClient(_serviceBusConfiguration.ConnectionString, _serviceBusConfiguration.Topic.Name);
        }

        public async Task SendMessage<TData>(TData payload)
        {
            try
            {
                var data = JsonConvert.SerializeObject(payload);
                var message = new Message(Encoding.UTF8.GetBytes(data));
                await _topicClient.SendAsync(message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
