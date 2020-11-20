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

        public ServiceBusManager(ServiceBusTopicSender serviceBusTopicSender, ServiceBusTopicSubscription serviceBusTopicSubscription)
        {
            _serviceBusTopicSubscription = serviceBusTopicSubscription;
            _serviceBusTopicSender = serviceBusTopicSender;
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

        public void RecieveMessage<TResult>(Func<TResult, Task> callBack)
        {
            _serviceBusTopicSubscription.RecieveMessage<TResult>(callBack);
        }
    }
}
