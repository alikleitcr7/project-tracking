using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectTracking.Managers
{
    public class SupervisingPolicyHandler : AuthorizationHandler<SupervisingPolicy>
    {
        private readonly IUserMethods userMethods;

        public SupervisingPolicyHandler(IUserMethods userMethods)
        {
            this.userMethods = userMethods;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SupervisingPolicy requirement)
        {
            //if (!context.User.Identity.IsAuthenticated)
            //{
            //    return Task.CompletedTask;
            //}

            string userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isSupervisor = userMethods.IsSupervisor(userId);

            if (!isSupervisor)
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
