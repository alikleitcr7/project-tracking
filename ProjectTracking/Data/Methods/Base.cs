using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data;
using ProjectTracking.Data.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking
{
    public class MethodsBase
    {
        //private ApplicationDbContext _context;

        //public MethodsBase()
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        //    optionsBuilder.UseSqlServer(Setting.ConnectionString);

        //    _context = new ApplicationDbContext(optionsBuilder.Options);
        //}

        //private Users _user;

        //public Users Users
        //{
        //    get
        //    {
        //        return _user ??
        //            (_user = new Users(_context));
        //    }
        //}

    }
}
