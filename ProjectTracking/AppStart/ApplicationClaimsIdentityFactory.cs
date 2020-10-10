using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectTracking.AppStart
{
    public class ApplicationClaimsIdentityFactory : Microsoft.AspNetCore.Identity.UserClaimsPrincipalFactory<ApplicationUser>
    {
        UserManager<ApplicationUser> _userManager;
        public ApplicationClaimsIdentityFactory(UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        { }
        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);


            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.Role,   ((ApplicationUserRole)user.RoleCode).ToString() )
            });

            return principal;
        }
    }
}
