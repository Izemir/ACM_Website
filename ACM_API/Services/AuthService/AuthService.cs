using ACM_API.DB;
using ACM_API.Dtos.User;
using ACM_API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public AuthService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetUserDto>> Login(string login, string password)
        {
            
            var response = new ServiceResponse<GetUserDto>();
            try
            {
                var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(login.ToLower()));
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }
                else if (!(user.Password == password))
                {
                    response.Success = false;
                    response.Message = "Wrong password.";
                }
                else
                {
                    response.Data = _mapper.Map<GetUserDto>(user);
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<long>> Register(AddUserDto newUser, string password)
        {            
            try
            {
                if (await UserExists(newUser.Email))
                {
                    return new ServiceResponse<long>
                    {
                        Success = false,
                        Message = "Такой пользователь уже существует."
                    };
                }

                _context.Users.Add(_mapper.Map<User>(newUser));
                await _context.SaveChangesAsync();

                var user = await _context.Users.FirstAsync(i => i.Email == newUser.Email);
                if (user == null)
                {
                    return new ServiceResponse<long>
                    {
                        Success = false,
                        Message = "Ошибка записи в базу"
                    };
                }
                else
                {
                    return new ServiceResponse<long> { Data = user.Id, Message = "Registration successful!" };
                }
                
            }
            catch (Exception ex)
            {
                return new ServiceResponse<long> { Success = false, Message = ex.Message };
            }

           
        }

        public async Task<bool> UserExists(string email)
        {
            if(await _context.Users.AnyAsync(i => i.Email.ToLower()
                .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }
    }
}
