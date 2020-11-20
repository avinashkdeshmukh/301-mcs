using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.CommonEntities;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ServiceBusMessaging.Model;
using ServiceBusMessaging;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace MT.OnlineRestaurant.ActionHandler
{
    public class OrderListener : IListener
    {
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly ITopicClient _topicClient;
        private readonly RestaurantManagementContext _restaurantManagementContext;
        private readonly IMapper _mapper;
        private readonly SubscriptionConfiguration _subscriptionConfiguration;
        private readonly TopicConfiguration _topicConfiguration;
        private ServiceBusMessageSender _serviceBusMessageSender;
        private readonly int _threshold = 1;

        public OrderListener(SubscriptionConfiguration subscriptionConfiguration,
            TopicConfiguration topicConfiguration,
            RestaurantManagementContext restaurantManagementContext,
            IMapper mapper)
        {
            this._subscriptionClient = new SubscriptionClient(subscriptionConfiguration.ConnectionString, subscriptionConfiguration.Topic, subscriptionConfiguration.Name);
            _topicClient = new TopicClient(topicConfiguration.ConnectionString, topicConfiguration.Name);
            this._subscriptionConfiguration = subscriptionConfiguration;
            this._topicConfiguration = topicConfiguration;
            this._restaurantManagementContext = restaurantManagementContext;
            this._mapper = mapper;
            _serviceBusMessageSender = new ServiceBusMessageSender(topicConfiguration);
        }

        public async Task ListenAsync()
        {
            var reciever = new ServiceBusMessageReciever(_subscriptionConfiguration);
            await reciever.RecieveMessage<Order>(OrderMessageHandler);
        }

        private async Task OrderMessageHandler(Order order)
        {
            if (null != order)
            {
                foreach (var menu in order.OrderMenuDetails)
                {
                    var existingMenu = _restaurantManagementContext.TblMenu.First(m => m.Id == menu.MenuId);
                    if ((existingMenu.quantity < (menu.QuantitySold + menu.Quantity)))
                    {
                        // Send message ITEM OUT OF STOCK
                        //var orderMenu = _mapper.Map<OrderMenus>(exis)
                        await _serviceBusMessageSender.SendMessage(menu);
                    }
                    else
                    {
                        //existingMenu.quantity -= (int)menu.Quantity;
                    }
                }
            }
        }
    }
}
