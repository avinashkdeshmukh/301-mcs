using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.ActionHandler
{
    public class MessageSender
    {
        public MessageSender()
        { 
        
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
