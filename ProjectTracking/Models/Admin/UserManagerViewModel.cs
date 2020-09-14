using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Admin
{
    public class UserManagerViewModel
    {
       public List<Category> _comapnyDto;
        public List<Team> _departmentDto;
        public IQueryable<ApplicationUser> _userManager;
        public UserManagerViewModel()
        {
          

        }


    }
}
