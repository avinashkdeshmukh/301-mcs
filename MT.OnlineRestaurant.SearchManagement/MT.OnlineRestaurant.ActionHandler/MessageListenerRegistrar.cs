using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.ActionHandler
{
    public class MessageListenerRegistrar
    {
        public static void RegisterListners(params IListener[] listeners)
        {
            foreach (var listener in listeners)
            {
                listener.ListenAsync().Wait();
                
            }
        }
    }
}
