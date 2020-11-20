using System;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.ActionHandler
{
    public interface IListener
    {
        Task ListenAsync();
    }
}
