using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusMessaging.Model;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
    //https://damienbod.com/2019/04/24/using-azure-service-bus-topics-in-asp-net-core/
    //https://tomasherceg.com/blog/post/azure-servicebus-in-net-core-managing-topics-queues-and-subscriptions-from-the-code
    public class ServiceBusTopicSubscription
    {
        private ServiceBusConfiguration _serviceBusConfiguration;
        private SubscriptionClient _subscriptionClient;
        private ServiceBusTopicSender _serviceBusTopicSender;
        //        private Func<TResult> _callBack = new Func<TResult>(TResult result) w

        public ServiceBusTopicSubscription(ServiceBusConfiguration serviceBusConfiguration)
        {
            _serviceBusConfiguration = serviceBusConfiguration;
            //_serviceBusTopicSender = serviceBusTopicSender;
            InitSubscription();
        }

        private void InitSubscription()
        {
            _subscriptionClient = new SubscriptionClient(_serviceBusConfiguration.ConnectionString, _serviceBusConfiguration.Topic.Name, _serviceBusConfiguration.Topic.Subscription.Name);
        }

        public void RecieveMessage<TResult>(Func<Message, CancellationToken, Task> callBack)
        {
            RegisterOnMessageHandlerAndReceiveMessages(callBack);
        }

        public void RecieveMessage<TResult>(Func<TResult, Task> callBack)
        {
            RegisterOnMessageHandlerAndReceiveMessages(callBack);
        }

        private void RegisterOnMessageHandlerAndReceiveMessages<TResult>(Func<TResult, Task> callBack)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionRecievedHanlder)
            {
                MaxConcurrentCalls = _serviceBusConfiguration.Topic.Subscription.MaxConcurrentCalls,
                AutoComplete = _serviceBusConfiguration.Topic.Subscription.AutoComplete

            };
            _subscriptionClient.RegisterMessageHandler(async (message, token) =>
            {
                var result = JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(message.Body));
                await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                await callBack(result);

            }, messageHandlerOptions);
        }

        private void RegisterOnMessageHandlerAndReceiveMessages(Func<Message, CancellationToken, Task> callBack)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionRecievedHanlder)
            {
                MaxConcurrentCalls = _serviceBusConfiguration.Topic.Subscription.MaxConcurrentCalls,
                AutoComplete = _serviceBusConfiguration.Topic.Subscription.AutoComplete

            };
            _subscriptionClient.RegisterMessageHandler(callBack, messageHandlerOptions);

        }

        private async Task<TResult> ProcessMessagesAsync<TResult>(Message message, CancellationToken token)
        {
            var result = JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(message.Body));
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            return result;

        }
        private Task ExceptionRecievedHanlder(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }

        //public Task CreateTopic()
        //{

        //}

        //public Task CreateSubscription()
        //{

        //}

        //public Task<bool> IsTopicExists()
        //{
        //}
    }
}
