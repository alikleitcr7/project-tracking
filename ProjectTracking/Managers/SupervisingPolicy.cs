using Microsoft.AspNetCore.Authorization;
using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTracking.Managers
{
    public class SupervisingPolicy : IAuthorizationRequirement
    {
    }
}
