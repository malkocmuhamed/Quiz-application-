using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web_API.Helpers;
using Web_API.Models;
using Web_API.Entities;

namespace Web_API.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {    
        private readonly Quiz_DBContext _context;
        private readonly AppSettings _appSettings;

        public UserService(Quiz_DBContext context, IOptions<AppSettings> options)
        {
            this._context = context;
            this._appSettings = options.Value;
        }     
        public IEnumerable<User> GetAll()
        {
            return _context.Users.WithoutPasswords();
        }
  
    }
}