using System;

namespace MT.OnlineRestaurant.ActionHandler
{
    public abstract class ActionHandler : IActionHandler
    {
        public virtual int Handle(object payload)
        {
            throw new NotImplementedException();
        }
    }
}
