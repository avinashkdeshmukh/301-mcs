using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.ActionHandler.Config;
using MT.OnlineRestaurant.CommonEntities;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MT.OnlineRestaurant.ActionHandler
{
    public class OrderListener : IListener
    {
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly ITopicClient _topicClient;
        private readonly RestaurantManagementContext _restaurantManagementContext;
        private readonly int _threshold = 1;

        public OrderListener(SubscriptionConfiguration subscriptionConfiguration, 
            TopicConfiguration topicConfiguration,
            RestaurantManagementContext restaurantManagementContext)
        {
            this._subscriptionClient = new SubscriptionClient(subscriptionConfiguration.ConnectionString, subscriptionConfiguration.Topic, subscriptionConfiguration.Name);
            _topicClient = new TopicClient(topicConfiguration.ConnectionString, topicConfiguration.Name);
            this._restaurantManagementContext = restaurantManagementContext;
        }

        public void Listen()
        {
            var messageHanlderOptions = new MessageHandlerOptions(Exceptionhanlder)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            };
            _subscriptionClient.RegisterMessageHandler(MessageHanlder, messageHanlderOptions);
        }

        private async Task MessageHanlder(Message msg, CancellationToken cancellationToken)
        {
            var order = JsonConvert.DeserializeObject<Order>(Encoding.UTF8.GetString(msg.Body));
            if (null != order)
            {
                foreach (var menu in order.OrderMenuDetails)
                {
                    var existingMenu = _restaurantManagementContext.TblMenu.First(m => m.Id == menu.MenuId);
                    if ((existingMenu.quantity < _threshold))
                    {
                        existingMenu.quantity -= (int)menu.Quantity;
                        
                    }
                }
            }
        }

        private Task Exceptionhanlder(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
