﻿using AutoMapper;
using ProjectTracking.Data.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.AppStart
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, DataContract.Project>().ForAllMembers(k => k.AllowNull());
            CreateMap<DataContract.Project, Project>().ForAllMembers(k => k.AllowNull());

            CreateMap<ApplicationUser, DataContract.User>().ForAllMembers(k => k.AllowNull());
            CreateMap<DataContract.User, ApplicationUser>().ForAllMembers(k => k.AllowNull());

            CreateMap<TimeSheet, DataContract.TimeSheet>().ForAllMembers(k => k.AllowNull());
            CreateMap<DataContract.TimeSheet, TimeSheet>().ForAllMembers(k => k.AllowNull());

            CreateMap<TimeSheetActivity, DataContract.TimeSheetActivity>().ForAllMembers(k => k.AllowNull());
            CreateMap<DataContract.TimeSheetActivity, TimeSheetActivity>().ForAllMembers(k => k.AllowNull());

            CreateMap<Team, DataContract.Team>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.Team, Team>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<Category, DataContract.Category>().ForAllMembers(k => k.AllowNull());
            CreateMap<DataContract.Category, Category>().ForAllMembers(k => k.AllowNull());

            CreateMap<UserLog, DataContract.UserLog>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.UserLog, UserLog>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<IpAddress, DataContract.IpAddress>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.IpAddress, IpAddress>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<DataContract.TimeSheetActivityLog, TimeSheetActivityLog>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<TimeSheetActivityLog, DataContract.TimeSheetActivityLog>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<TimeSheetActivity, TimeSheetActivityLog>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<TimeSheetActivity, DataContract.TimeSheetActivityLog>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<Notification, DataContract.Notification>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.Notification, Notification>().ForAllMembers(opt => opt.AllowNull());
            
            CreateMap<ProjectTask, DataContract.ProjectTask>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.ProjectTask, ProjectTask>().ForAllMembers(opt => opt.AllowNull());
            
            CreateMap<TeamsProjects, DataContract.TeamsProjects>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.TeamsProjects, TeamsProjects>().ForAllMembers(opt => opt.AllowNull());
            
            CreateMap<TimeSheetTask, DataContract.TimeSheetTask>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.TimeSheetTask, TimeSheetTask>().ForAllMembers(opt => opt.AllowNull());
        }
    }
}
