using Microsoft.AspNet.Identity;
using ASPNETMVCIdentity.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASPNETMVCIdentity.App_Start
{
    public class IdentityUserStore : UserStore
    {
        public IdentityUserStore(LoginUserDbContext context)
            : base(context) { }
    }

    public class IdentityUserManager : UserManager
    {
        public IdentityUserManager(IUserStore store)
            : base(store) { }
    }
}