using ACM_API.DB;
using ACM_API.Dtos.Chat;
using ACM_API.Models;
using ACM_API.Models.Chat;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public ChatService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        

        public async Task<ServiceResponse<ChatDto>> GetChat(long chatId, long requestId)
        {
            var serviceResponse = new ServiceResponse<ChatDto>();
            try
            {
                var chat = await _context.Chats
                 .Include(i => i.Messages)
                 .Include(i=> i.Executor)
                 .Include(i=>i.Customer)
                 .ThenInclude(e => e.User)
                 .FirstAsync(i => i.Id == chatId);
                var messages = chat.Messages.Select(i => _mapper.Map<MessageDto>(i)).ToList();
                var data = _mapper.Map<ChatDto>(chat);
                data.Messages = messages;
                if(chat.Customer.User.Id == requestId)
                {
                    data.SenderName = chat.Customer.Name;
                    data.ChatName = GetChatName(chat.Executor.Name);
                }
                else
                {
                    data.SenderName = chat.Executor.Name;
                    data.ChatName = GetChatName(chat.Customer.Name);
                }

                serviceResponse.Data = data;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ChatDto>>> GetCustomerChats(long customerId)
        {
            var serviceResponse = new ServiceResponse<List<ChatDto>>();
            try
            {
                var customer = await _context.Customers
                 .Include(i => i.Chats)
                 .ThenInclude(c=>c.Executor)
                 .FirstAsync(i => i.Id == customerId);

                List<ChatDto> chats = new List<ChatDto>();
                foreach(var chat in customer.Chats)
                {
                    var chatDto = _mapper.Map<ChatDto>(chat);
                    chatDto.ChatName = GetChatName(chat.Executor.Name);
                    chats.Add(chatDto);
                }

                serviceResponse.Data = chats;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ChatDto>>> GetExecutorChats(long executorId)
        {
            var serviceResponse = new ServiceResponse<List<ChatDto>>();
            try
            {
                var customer = await _context.Executors
                 .Include(i => i.Chats)
                 .ThenInclude(c => c.Customer)
                 .FirstAsync(i => i.Id == executorId);

                List<ChatDto> chats = new List<ChatDto>();
                foreach (var chat in customer.Chats)
                {
                    var chatDto = _mapper.Map<ChatDto>(chat);
                    chatDto.ChatName = GetChatName(chat.Customer.Name);
                    chats.Add(chatDto);
                }

                serviceResponse.Data = chats;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<ChatDto>> SendMessage(long chatId, MessageDto message)
        {
            var serviceResponse = new ServiceResponse<ChatDto>();
            try
            {
                var chat = await _context.Chats
                 .Include(i => i.Messages)
                 .Include(i => i.Executor)
                 .Include(i => i.Customer)
                 .FirstAsync(i => i.Id == chatId);

                chat.Messages.Add(_mapper.Map<Message>(message));
                _context.Entry(chat).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var messages = chat.Messages.Select(i => _mapper.Map<MessageDto>(i)).ToList();
                var data = _mapper.Map<ChatDto>(chat);
                data.Messages = messages;
                data.ChatName = chat.Customer.Name != message.SenderName ? GetChatName(chat.Customer.Name) : GetChatName(chat.Executor.Name);
                data.SenderName = message.SenderName;

                serviceResponse.Data = data;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<ChatDto>> StartNewChat(long customerId, long executorId)
        {
            var serviceResponse = new ServiceResponse<ChatDto>();
            try
            {
                var customer = await _context.Customers
                    .Include(i=>i.Chats)
                    .FirstOrDefaultAsync(i => i.Id == customerId);
                var executor = await _context.Executors
                    .Include(i => i.Chats)
                    .FirstOrDefaultAsync(i => i.Id == executorId);

                var chat = new Chat() { Customer = customer, Executor = executor };

                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();

                var data = _mapper.Map<ChatDto>(chat);
                data.ChatName = GetChatName(chat.Executor.Name);
                data.SenderName = chat.Customer.Name;

                serviceResponse.Data = data;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<long>> ExistChat(long customerId, long executorId)
        {
            var serviceResponse = new ServiceResponse<long>();
            try
            {
                var chat = await _context.Chats
                    .Include(i => i.Customer)
                    .Include(i => i.Executor)
                    .FirstOrDefaultAsync(i => i.Customer.Id == customerId && i.Executor.Id == executorId);

                if (chat == null)
                {
                    serviceResponse.Data = 0;
                }
                else
                {
                    serviceResponse.Data = chat.Id;
                }


                

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private string GetChatName(string name)
        {
            return "Чат с " + name;
        }
    }
}
