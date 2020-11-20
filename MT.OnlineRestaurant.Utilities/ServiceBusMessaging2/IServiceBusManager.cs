using System;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
    public interface IServiceBusManager
    {
        Task SendMessage<TData>(TData data);

        void RecieveMessage<TResult>(Func<TResult, Task> callBack);
    }
}