using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusMessaging.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
    public class ServiceBusMessageSender
    {
        //private readonly ServiceBusConfiguration _serviceBusConfiguration;
        
        private readonly TopicConfiguration _topicConfiguration;
        private TopicClient _topicClient;

        //public ServiceBusTopicSender(ServiceBusConfiguration serviceBusConfiguration)
        //{
        //    this._serviceBusConfiguration = serviceBusConfiguration;
        //    ConfigureTopic();
        //}
        public ServiceBusMessageSender(TopicConfiguration topicConfiguration)
        {
            //this._serviceBusConfiguration = serviceBusConfiguration;
            this._topicConfiguration = topicConfiguration;
            ConfigureTopic();
        }

        private void ConfigureTopic()
        {
            //_topicClient = new TopicClient(_serviceBusConfiguration.ConnectionString, _serviceBusConfiguration.Topic.Name);
            _topicClient = new TopicClient(_topicConfiguration.ConnectionString, _topicConfiguration.Name);
        }

        public async Task SendMessage(object payload)
        {
            var data = JsonConvert.SerializeObject(payload);
            var message = new Message(Encoding.UTF8.GetBytes(data));
            await _topicClient.SendAsync(message);
        }
    }
}
