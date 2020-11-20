using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.ActionHandler
{
    public class OrderActionHandler : ActionHandler, IOrderActionHandler
    {
        private readonly ITopicClient _topicClient;

        public OrderActionHandler(ITopicClient topicClient)
        {
            this._topicClient = topicClient;
        }

        public override int Handle(object payload)
        {
            var order = (OrderEntity)payload;
            try
            {
                Task.Run(() => NotifyOrderPlaced(order));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        private async void NotifyOrderPlaced(OrderEntity order)
        {
             SendMessageAsync(order);
        }

        private async void SendMessageAsync(object payload)
        {
            try
            {
                var objString = JsonConvert.SerializeObject(payload);
                var message = new Message(Encoding.UTF8.GetBytes(objString));
                await _topicClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
