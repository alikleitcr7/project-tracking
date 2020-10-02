using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectTracking.AppStart;
using ProjectTracking.Data;
using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectTracking.Managers
{
    public class SupervisingPolicyHandler : AuthorizationHandler<SupervisingPolicy>
    {
        private ApplicationDbContext db;

        public SupervisingPolicyHandler(IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Setting.ConnectionString);
            db = new ApplicationDbContext(optionsBuilder.Options, config);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SupervisingPolicy requirement)
        {
            //if (!context.User.Identity.IsAuthenticated)
            //{
            //    return Task.CompletedTask;
            //}

            if (requirement.OrAdminFlag && context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            string userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isSupervisor = db.Supervisers.Any(k => k.UserId == userId);

            if (!isSupervisor)
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
