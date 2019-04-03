using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ASPNETMVCIdentity.Models
{
    public class LoginUser : IdentityUser
    {

    }
    public class LoginUserDbContext : IdentityDbContext
    {
        public LoginUserDbContext()
            : base("DefaultConnection")
        {
        }
    }
}