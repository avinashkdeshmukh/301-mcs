using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.ActionHandler
{
    public class MessageListenerRegistrar
    {
        public static void RegisterListners(params IListener[] listeners)
        {
            foreach (var listener in listeners)
            {
                listener.Listen();
            }
        }
    }
}
