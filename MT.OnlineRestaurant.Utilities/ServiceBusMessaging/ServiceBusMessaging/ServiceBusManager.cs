using ServiceBusMessaging.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
    public class ServiceBusManager : IServiceBusManager
    {
        private ServiceBusTopicSubscription _serviceBusTopicSubscription;
        private ServiceBusTopicSender _serviceBusTopicSender;
        private readonly ServiceBusConfiguration _serviceBusConfiguration;
        private Dictionary<string, ServiceBusTopicSender> _topics;
        private Dictionary<string, ServiceBusTopicSubscription> _subscriptions;

        public ServiceBusManager(ServiceBusTopicSender serviceBusTopicSender, ServiceBusTopicSubscription serviceBusTopicSubscription)
        {
            _serviceBusTopicSubscription = serviceBusTopicSubscription;
            _serviceBusTopicSender = serviceBusTopicSender;
        }

        public ServiceBusManager(ServiceBusConfiguration serviceBusConfiguration)
        {
            this._serviceBusConfiguration = serviceBusConfiguration;
            _serviceBusTopicSubscription = new ServiceBusTopicSubscription(_serviceBusConfiguration);
            _serviceBusTopicSender = new ServiceBusTopicSender(serviceBusConfiguration);
        }

        //private async Task<TResult> SendAndRecieveMessage<TData, TResult>(TData data)
        //{
        //    //await _serviceBusTopicSender.SendMessage<TData>(data);
        //    //var result = await _serviceBusTopicSubscription.RecieveMessage<TData, TResult>(data);
        //    //return result;
        //}

        public async Task SendMessage<TData>(TData data)
        {
            await _serviceBusTopicSender.SendMessage<TData>(data);  
        }
        public async Task RecieveMessage<TResult>( Func<TResult, Task> callBack)
        {
            _serviceBusTopicSubscription.RecieveMessage<TResult>(callBack);
        }

        public async Task SendMessage<TData>(string topicName, TData data)
        {
            var topic = _topics[topicName];
            await topic.SendMessage<TData>(data);
        }

        public async Task RecieveMessage<TResult>(string topicName, string subscriptionName, Func<TResult, Task> callBack)
        {
            var topic = _topics[topicName];
            var subscription = topic.Subscriptions[subscriptionName];
            subscription.RecieveMessage<TResult>(callBack);
        }
    }
}
