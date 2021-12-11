using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizApp.IdentityServer.QuizAppIdentity
{
    public class QuizAppProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<QuizUser> _claimsFactory;
        private readonly UserManager<QuizUser> _userManager;

        public QuizAppProfileService(UserManager<QuizUser> userManager, IUserClaimsPrincipalFactory<QuizUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var quizUser = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(quizUser);
            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim("username", quizUser.UserName));
            claims.Add(new Claim("userId", quizUser.Id));
            claims.Add(new Claim("isAdmin", quizUser.IsAdministator.ToString()));

            if (quizUser.IsAdministator)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "user"));
            }

            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, quizUser.Email));
            claims.Add(new Claim(IdentityServerConstants.StandardScopes.OfflineAccess, "true"));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = true;
        }
    }
}
