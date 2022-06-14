using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.ChatService
{
    public interface IChatService
    {

        Task<ServiceResponse<List<WebChat>>> GetCustomerChats(long customerId);

        Task<ServiceResponse<List<WebChat>>> GetExecutorChats(long executorId);

        Task<ServiceResponse<WebChat>> GetChat(long chatId, long requestId);

        Task<ServiceResponse<WebChat>> SendMessage(long chatId, Message message);

        Task<ServiceResponse<WebChat>> StartNewChat(long customerId, long executorId);

        Task<ServiceResponse<long>> ExistChat(long customerId, long executorId);

        Task<ServiceResponse<Sender>> GetSenderInfo(long userId);
    }
}
