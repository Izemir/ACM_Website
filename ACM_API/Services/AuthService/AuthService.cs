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

        public async Task<ServiceResponse<bool>> IsAdmin(long userId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var user = await _context.Users.FirstAsync(i => i.Id == userId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }
                else
                {
                    if (user.Role == "Администратор") response.Data = true;
                    else response.Data = false;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<UserDto>> Login(string email, string password)
        {
            
            var response = new ServiceResponse<UserDto>();
            try
            {
                var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
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
                    response.Data = _mapper.Map<UserDto>(user);
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<UserDto>> Register(UserDto newUser, string password)
        {

            var response = new ServiceResponse<UserDto>();
            try
            {
                if (await UserExists(newUser.Email))
                {
                    return new ServiceResponse<UserDto>
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
                    return new ServiceResponse<UserDto>
                    {
                        Success = false,
                        Message = "Ошибка записи в базу"
                    };
                }
                else response.Data=_mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;         

           
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
