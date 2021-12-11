using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Web_API.Services;
using Web_API.Entities;
using Web_API.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.AspNetCore.Http;
using Web_API.Helpers;

using Web_API.Enum;
namespace Web_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly Quiz_DBContext _context;

        private readonly AppSettings _appSettings;

        
        public UsersController(Quiz_DBContext context, IUserService userservice, IOptions<AppSettings> appSettings)
        {
             _context = context;
             _userService = userservice;
            _appSettings = appSettings.Value;      
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserbyID(int id)
        {
            if(id < 0)
            {
                id = SharedHelpers.GetUserIdFromToken(User.Identity as ClaimsIdentity);
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        
        [HttpPost]
        //POST: api/users
        public async Task <ActionResult<User>> PostUser(User user)
        {
            try
            {
                if (user==null)
                {
                    return BadRequest();
                }
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUserbyID", new { id = user.Id }, user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        [Route("api/UpdateOutput")]
        public void UpdateOutput(User model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();            
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
