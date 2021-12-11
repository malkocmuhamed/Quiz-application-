﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.IdentityServer.QuizAppIdentity
{
    public class QuizUser : IdentityUser
    {
        public bool IsAdministator { get; set; }
    }
}
