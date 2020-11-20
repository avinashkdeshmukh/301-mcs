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
    public class ServiceBusMessageReciever
    {
        //private ServiceBusConfiguration _serviceBusConfiguration;
        //private ServiceBusMessageSender _serviceBusTopicSender;

        private SubscriptionClient _subscriptionClient;
        private readonly SubscriptionConfiguration _subscriptionConfiguration;

        //public ServiceBusMessageReciever(ServiceBusConfiguration serviceBusConfiguration, ServiceBusMessageSender serviceBusTopicSender)
        //{
        //    _serviceBusConfiguration = serviceBusConfiguration;
        //    _serviceBusTopicSender = serviceBusTopicSender;
        //    InitSubscription();
        //}

        public ServiceBusMessageReciever(SubscriptionConfiguration subscriptionConfiguration)
        {
            this._subscriptionConfiguration = subscriptionConfiguration;
            InitSubscription();
        }
        private void InitSubscription()
        {
            _subscriptionClient = new SubscriptionClient(_subscriptionConfiguration.ConnectionString, _subscriptionConfiguration.Topic, _subscriptionConfiguration.Name);
        }

        public void RecieveMessage<TResult>(Action<TResult> messageReciever)
        {
            RegisterOnMessageHandlerAndReceiveMessages<TResult>(messageReciever);
        }


        private void RegisterOnMessageHandlerAndReceiveMessages<TResult>(Action<TResult> messageReciever)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionRecievedHanlder)
            {
                MaxConcurrentCalls = _subscriptionConfiguration.MaxConcurrentCalls,
                AutoComplete = _subscriptionConfiguration.AutoComplete
            };
            _subscriptionClient.RegisterMessageHandler(async (message, token) =>
                {
                    var result = JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(message.Body));
                    await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                    messageReciever(result);
                    //return result;
                }, messageHandlerOptions);
        }

        public async Task RecieveMessage<TResult>(Func<TResult, Task> messageReciever)
        {
            await RegisterOnMessageHandlerAndReceiveMessages<TResult>(messageReciever);
        }


        private async Task RegisterOnMessageHandlerAndReceiveMessages<TResult>(Func<TResult, Task> messageReciever)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionRecievedHanlder)
            {
                MaxConcurrentCalls = _subscriptionConfiguration.MaxConcurrentCalls,
                AutoComplete = _subscriptionConfiguration.AutoComplete
            };
             _subscriptionClient.RegisterMessageHandler(async (message, token) =>
            {
                var result = JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(message.Body));
                await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                await messageReciever(result);
                //return result;
            }, messageHandlerOptions);
        }

        //private async Task<TResult> ProcessMessagesAsync<TResult>(Message message, CancellationToken token)
        //{
        //    var result = JsonConvert.DeserializeObject<TResult>(Encoding.UTF8.GetString(message.Body));
        //    await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        //    return result;

        //}
        private Task ExceptionRecievedHanlder(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
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
