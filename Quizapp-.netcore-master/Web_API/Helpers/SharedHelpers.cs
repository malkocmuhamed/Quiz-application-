using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web_API.Helpers
{
    public class SharedHelpers
    {
        public static int GetUserIdFromToken(ClaimsIdentity identity)
        {
            return Convert.ToInt32(identity.Claims.Where(c => c.Type == "sub").Single().Value);
        }
    }
}
