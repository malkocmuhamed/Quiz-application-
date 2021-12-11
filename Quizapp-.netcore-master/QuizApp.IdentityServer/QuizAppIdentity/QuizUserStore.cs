using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web_API.Models;

namespace QuizApp.IdentityServer.QuizAppIdentity
{
    public class QuizUserStore : IUserStore<QuizUser>, IUserPasswordStore<QuizUser>
    {
        private readonly Quiz_DBContext context;
        private IHttpContextAccessor httpContextAccessor;
        private IConfiguration configuration;
        private QuizUser quizUser = new QuizUser();

        public QuizUserStore(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, Quiz_DBContext context)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.context = context;
        }

        public Task<string> GetUserIdAsync(QuizUser quizUser, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(quizUser.Id);
        }

        public Task<string> GetUserNameAsync(QuizUser quizUser, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(quizUser.UserName);
        }

        public Task SetUserNameAsync(QuizUser quizUser, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(QuizUser quizUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(QuizUser quizUser, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(QuizUser quizUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(QuizUser quizUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(QuizUser quizUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<QuizUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            User user = context.Users.Where(u => u.Id.ToString().ToLower() == userId.ToLower()).FirstOrDefaultAsync().Result;
            quizUser.Id = user.Id.ToString();
            quizUser.Email = user.Email;
            quizUser.UserName = user.Name;
            quizUser.IsAdministator = user.UserTypeId == 1 ? true : false;
            quizUser.PasswordHash = new PasswordHasher<QuizUser>().HashPassword(quizUser, user.Password);

            return Task.FromResult(quizUser);
        }

        public Task<QuizUser> FindByNameAsync(string email, CancellationToken cancellationToken)
        {
            User user = context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync().Result;
            quizUser.Id = user.Id.ToString();
            quizUser.Email = user.Email;
            quizUser.PasswordHash = new PasswordHasher<QuizUser>().HashPassword(quizUser, user.Password);

            return Task.FromResult(quizUser);
        }

        public void Dispose()
        {

        }

        public Task SetPasswordHashAsync(QuizUser user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.PasswordHash = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(QuizUser user, CancellationToken cancellationToken)
        {
            return user.PasswordHash != null ? Task.FromResult<string>(user.PasswordHash.ToString()) : null;
        }

        public Task<bool> HasPasswordAsync(QuizUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
