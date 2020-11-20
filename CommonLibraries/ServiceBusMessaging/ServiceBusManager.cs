using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
    public class ServiceBusManager
    {
        private ServiceBusMessageReciever _serviceBusTopicSubscription;
        private ServiceBusMessageSender _serviceBusTopicSender;

        public ServiceBusManager(ServiceBusMessageSender serviceBusTopicSender, ServiceBusMessageReciever serviceBusTopicSubscription)
        {
            _serviceBusTopicSubscription = serviceBusTopicSubscription;
            _serviceBusTopicSender = serviceBusTopicSender;
        }

        public async Task<TResult> SendAndRecieveMessage<TResult>(object data)
        {
            TResult result = default(TResult);
            await _serviceBusTopicSender.SendMessage(data);
            await _serviceBusTopicSubscription.RecieveMessage<TResult>(async (recienveResult) =>
            {
                //return Task.FromResult(recienveResult);
                result  = recienveResult;
            });

            return await Task.FromResult(result);
        }
    }
}
