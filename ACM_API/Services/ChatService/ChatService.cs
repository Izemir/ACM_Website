using ACM_API.DB;
using ACM_API.Dtos.Chat;
using ACM_API.Dtos.Customer;
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
                 .Include(i=>i.SubCustomers)
                 .FirstAsync(i => i.Id == chatId);
                var messages = chat.Messages.Select(i => _mapper.Map<MessageDto>(i)).ToList();
                var subs = chat.SubCustomers.Select(i => _mapper.Map<SubCustomerDto>(i)).ToList();
                var data = _mapper.Map<ChatDto>(chat);
                data.Messages = messages;
                data.SubCustomers = subs;
                if(chat.Customer.User.Id == requestId)
                {
                    data.ChatName = chat.NameForCustomer;
                }
                else
                {
                    data.ChatName = chat.NameForExecutor;
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
                    chatDto.ChatName = chat.NameForCustomer;
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
                var executor = await _context.Executors
                 .Include(i => i.Chats)
                 .ThenInclude(c => c.Customer)
                 .FirstAsync(i => i.Id == executorId);

                List<ChatDto> chats = new List<ChatDto>();
                foreach (var chat in executor.Chats)
                {
                    var chatDto = _mapper.Map<ChatDto>(chat);
                    chatDto.ChatName = chat.NameForExecutor;
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
                 .Include(i=>i.SubCustomers)
                 .FirstAsync(i => i.Id == chatId);

                chat.Messages.Add(_mapper.Map<Message>(message));
                _context.Entry(chat).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var messages = chat.Messages.Select(i => _mapper.Map<MessageDto>(i)).ToList();
                var data = _mapper.Map<ChatDto>(chat);
                data.Messages = messages;
                data.ChatName = chat.Executor.Id == message.SenderId ? chat.NameForExecutor : chat.NameForCustomer;
                var subs = chat.SubCustomers.Select(i => _mapper.Map<SubCustomerDto>(i)).ToList();
                data.SubCustomers = subs;

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
                //data.SenderName = chat.Customer.Name;

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

        public async Task<ServiceResponse<SenderDto>> GetSender(long userId)
        {
            var serviceResponse = new ServiceResponse<SenderDto>();
            try
            {
                SenderDto sender = new SenderDto();
                var user = await _context.Users
                    .Include(i=>i.Customer)
                    .Include(i=>i.Executor)
                    .FirstOrDefaultAsync(i => i.Id == userId);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found.";
                    return serviceResponse;
                }

                if (user.Role == "Заказчик")
                {
                    sender.SenderName = user.Customer.Name;
                    sender.SenderId = user.Customer.Id;
                }
                if (user.Role == "Исполнитель")
                {
                    sender.SenderName = user.Executor.Name;
                    sender.SenderId = user.Executor.Id;
                }
                if (user.Role == "Администратор")
                {
                    sender.SenderName = "Модератор";
                    sender.SenderId = 0;
                }


                serviceResponse.Data= sender;



            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ChatDto>>> GetSubCustomerChats(long subCustomerId)
        {
            var serviceResponse = new ServiceResponse<List<ChatDto>>();
            try
            {
                var sub = await _context.SubCustomers
                 .Include(i => i.Chats)
                 .FirstAsync(i => i.Id == subCustomerId);

                List<ChatDto> chats = new List<ChatDto>();
                foreach (var chat in sub.Chats)
                {
                    var chatDto = _mapper.Map<ChatDto>(chat);
                    chatDto.ChatName = chat.NameForCustomer;
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

        public async Task<ServiceResponse<ChatDto>> AddSubToChat(long chatId, long subCustomerId)
        {
            var serviceResponse = new ServiceResponse<ChatDto>();
            try
            {
                var chat = await _context.Chats
                    .Include(i => i.SubCustomers)
                    .FirstOrDefaultAsync(i => i.Id == chatId);

                var sub = await _context.SubCustomers
                    .Include(i => i.Chats)
                    .FirstOrDefaultAsync(i => i.Id == subCustomerId);

                chat.SubCustomers.Add(sub);
                _context.Entry(chat).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var data = _mapper.Map<ChatDto>(chat);
                var subs = chat.SubCustomers.Select(i => _mapper.Map<SubCustomerDto>(i)).ToList();
                data.SubCustomers = subs;

                serviceResponse.Data = data;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<ChatDto>> DeleteSubFromChat(long chatId, long subCustomerId)
        {
            var serviceResponse = new ServiceResponse<ChatDto>();
            try
            {
                var chat = await _context.Chats
                    .Include(i => i.SubCustomers)
                    .FirstOrDefaultAsync(i => i.Id == chatId);

                var sub = await _context.SubCustomers
                    .Include(i => i.Chats)
                    .FirstOrDefaultAsync(i => i.Id == subCustomerId);

                chat.SubCustomers.Remove(sub);
                _context.Entry(chat).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var data = _mapper.Map<ChatDto>(chat);
                var subs = chat.SubCustomers.Select(i => _mapper.Map<SubCustomerDto>(i)).ToList();
                data.SubCustomers = subs;

                serviceResponse.Data = data;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
