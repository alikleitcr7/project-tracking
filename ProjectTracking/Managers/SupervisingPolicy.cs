using Microsoft.AspNetCore.Authorization;
using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTracking.Managers
{
    public class SupervisingPolicy : IAuthorizationRequirement
    {
        public const string SUPERVISING = "SupervisingPolicy";
        public const string SUPERVISING_OR_ADMIN = "SupervisingOrAdminPolicy";

        public SupervisingPolicy(bool orAdminFlag)
        {
            OrAdminFlag = orAdminFlag;
        }

        public bool OrAdminFlag { get; }
    }
}
