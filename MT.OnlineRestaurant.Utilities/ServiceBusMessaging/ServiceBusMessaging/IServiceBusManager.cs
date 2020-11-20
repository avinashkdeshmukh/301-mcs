using System;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
    public interface IServiceBusManager
    {
        Task SendMessage<TData>(TData data);

        Task RecieveMessage<TResult>(Func<TResult, Task> callBack);
    }
}