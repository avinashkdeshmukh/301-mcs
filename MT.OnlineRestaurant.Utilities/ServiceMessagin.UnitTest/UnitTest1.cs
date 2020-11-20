using CommonEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceBusMessaging;
using ServiceBusMessaging.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMessagin.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            ServiceBusConfiguration config = new ServiceBusConfiguration();
            //config.ConnectionString = "Endpoint=sb://restorder.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=xwradxwyM1GusLWGxwOYXjLwMOoFRWQUyMK3XwOvObM=";
            config.ConnectionString = "Endpoint=sb://restorder2.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=u7jx+8mXYgSIDu4zL6TXayqkIy/F/tqW/mAAxggHvZs=";
            config.Topic = new TopicConfiguration()
            {
                Name = "order",
                Subscription = new SubscriptionConfiguration()
                {
                    Name = "outofstock",
                    AutoComplete = false,
                    MaxConcurrentCalls = 1
                }
            };

            ServiceBusTopicSender serviceBusTopicSender = new ServiceBusTopicSender(config);
            ServiceBusTopicSubscription serviceBusTopicSubscription = new ServiceBusTopicSubscription(config);
            IServiceBusManager mgr = new ServiceBusManager(serviceBusTopicSender, serviceBusTopicSubscription);
            try
            {
                ICollection<OrderMenu> menus = new List<OrderMenu>();
                menus.Add(new OrderMenu { MenuId = 1, Price = 10 });

                for (int i = 1; i <= 2; i++)
                {
                    //mgr.SendMessage<OrderEntity>(new OrderEntity
                    //{
                    //    CustomerId = i,
                    //    DeliveryAddress = "Subramanyapura",
                    //    OrderMenuDetails = menus,
                    //    RestaurantId = i
                    //});
                    await mgr.SendMessage<string>("First message");
                }

                //mgr.RecieveMessage<OrderEntity>(async order =>
                //{
                //    Console.WriteLine("Order received", order.RestaurantId, order.OrderMenuDetails.First().MenuId);
                //    await Task.CompletedTask;
                //});

                mgr.RecieveMessage<string>(async order =>
                {
                    Console.WriteLine("Order received", order);
                    await Task.CompletedTask;
                });
                Assert.IsTrue(true);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Excpetion occured", ex);
                throw;
            }
        }
    }
}
