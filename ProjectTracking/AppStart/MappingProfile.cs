using AutoMapper;
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

            CreateMap<TimeSheetStatus, DataContract.TimeSheetStatus>().ForAllMembers(k => k.AllowNull());
            CreateMap<DataContract.TimeSheetStatus, TimeSheetStatus>().ForAllMembers(k => k.AllowNull());

            CreateMap<TimeSheetProject, DataContract.TimeSheetProject>().ForAllMembers(k => k.AllowNull());
            CreateMap<DataContract.TimeSheetProject, TimeSheetProject>().ForAllMembers(k => k.AllowNull());

            CreateMap<Team, DataContract.Team>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.Team, Team>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<Category, DataContract.Category>().ForAllMembers(k => k.AllowNull());
            CreateMap<DataContract.Category, Category>().ForAllMembers(k => k.AllowNull());

            CreateMap<UserLog, DataContract.UserLog>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.UserLog, UserLog>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<Permission, DataContract.Permission>();
            //CreateMap<DataContract.Permission, Permission>().ForMember(c => c.ID, opt => opt.Ignore());

            CreateMap<TimeSheetStatus, DataContract.TimeSheetStatus>();
            CreateMap<DataContract.TimeSheetStatus, TimeSheetStatus>().ForMember(c => c.ID, opt => opt.Ignore());

            //CreateMap<RequestedPermissionsStatusCode, DataContract.RequestedPermissionsStatusCode>();
            //CreateMap<DataContract.RequestedPermissionsStatusCode, RequestedPermissionsStatusCode>();

            //CreateMap<RequestedPermission, DataContract.RequestedPermission>();
            //CreateMap<DataContract.RequestedPermission, RequestedPermission>().ForMember(c => c.ID, opt => opt.Ignore());//RequestedPermissionsStatus

            //CreateMap<DataContract.RequestedPermissionsStatus, RequestedPermissionsStatus>().ForMember(c => c.Id, opt => opt.Ignore());//RequestedPermissionsStatus
            //CreateMap<RequestedPermissionsStatus, DataContract.RequestedPermissionsStatus>();

            CreateMap<ProjectReference, DataContract.ProjectFile>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.ProjectFile, ProjectReference>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<IpAddress, DataContract.IpAddress>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.IpAddress, IpAddress>().ForAllMembers(opt => opt.AllowNull());


            //CreateMap<Country, DataContract.Country>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<DataContract.Country, Country>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<DataContract.InventoryType, InventoryType>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<InventoryType, DataContract.InventoryType>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<DataContract.InventoryStatus, InventoryStatus>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<InventoryStatus, DataContract.InventoryStatus>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<DataContract.UpdateFrequency, UpdateFrequency>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<UpdateFrequency, DataContract.UpdateFrequency>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<DataContract.PublishingChannel, PublishingChannel>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<PublishingChannel, DataContract.PublishingChannel>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<DataContract.InventoryProject, InventoryProject>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<InventoryProject, DataContract.InventoryProject>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<InventoryProjectPublishingChannel, DataContract.InventoryProjectPublishingChannel>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<DataContract.InventoryProjectPublishingChannel, InventoryProjectPublishingChannel>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<InventorySubProject, DataContract.InventorySubProject>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<DataContract.InventorySubProject, InventorySubProject>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<TypeOfWork, DataContract.TypeOfWork>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.TypeOfWork, TypeOfWork>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<MeasurementUnit, DataContract.MeasurementUnit>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.MeasurementUnit, MeasurementUnit>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<InventoryProjectSubProjects, DataContract.InventoryProjectSubProjects>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<DataContract.InventoryProjectSubProjects, InventoryProjectSubProjects>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<DataContract.TimeSheetActivityLog, TimeSheetActivityLog>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<TimeSheetActivityLog, DataContract.TimeSheetActivityLog>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<TimeSheetActivity, TimeSheetActivityLog>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<TimeSheetActivity, DataContract.TimeSheetActivityLog>().ForAllMembers(opt => opt.AllowNull());

            CreateMap<Notification, DataContract.Notification>().ForAllMembers(opt => opt.AllowNull());
            CreateMap<DataContract.Notification, Notification>().ForAllMembers(opt => opt.AllowNull());

            //CreateMap<Holiday, DataContract.Holiday>().ForAllMembers(opt => opt.AllowNull());
            //CreateMap<DataContract.Holiday, Holiday>().ForAllMembers(opt => opt.AllowNull());
        }
    }
}
