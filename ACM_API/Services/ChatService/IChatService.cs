using ACM_API.Dtos.Chat;
using ACM_API.Models;
using ACM_API.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.ChatService
{
    public interface IChatService
    {
        Task<ServiceResponse<List<ChatDto>>> GetCustomerChats(long customerId);

        Task<ServiceResponse<List<ChatDto>>> GetExecutorChats(long executorId);

        Task<ServiceResponse<ChatDto>> GetChat(long chatId, long requestId);

        Task<ServiceResponse<ChatDto>> SendMessage(long chatId,MessageDto message);

        Task<ServiceResponse<ChatDto>> StartNewChat(long constructionId, long executorId);

        Task<ServiceResponse<long>> ExistChat(long constructionId, long executorId);
        Task<ServiceResponse<SenderDto>> GetSender(long userId);

        Task<ServiceResponse<List<ChatDto>>> GetSubCustomerChats(long subCustomerId);

        Task<ServiceResponse<ChatDto>> AddSubToChat(long chatId, long subCustomerId);
        Task<ServiceResponse<ChatDto>> DeleteSubFromChat(long chatId, long subCustomerId);
    }
}
