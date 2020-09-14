using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.DataSets;
using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Data.Methods
{


    public class ProjectFilesMethods : GenericRepository<DataSets.ProjectReference>, IProjectFilesMethods
    {
        private readonly IMapper _mapper;

        public ProjectFilesMethods(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public List<ProjectReference> Search(string keyword, int? projectId)
        {
            if (!projectId.HasValue)
            {
                return _context.ProjectReference.Where(k => k.Name != null && k.Name.ToLower().IndexOf(keyword.ToLower()) > -1).ToList();
            }

            return _context.ProjectReference.Include(k => k.Project).Where(k =>
            (k.ProjectId == projectId.Value ||
            (k.Project != null && k.Project.ParentId == projectId.Value))
            && k.Name != null && k.Name.ToLower().IndexOf(keyword.ToLower()) > -1)
                .ToList();
        }

        public DataContract.ProjectFile GetFileWithActivities(int fileId)
        {
            DataSets.ProjectReference file = _context.ProjectReference.Include(k => k.TimeSheetActivities)
                                .ThenInclude(k => k.TimeSheetProject)
                                .Include(k => k.TimeSheetActivities)
                                .ThenInclude(k => k.MeasurementUnit)
                                .Include(k => k.TimeSheetActivities)
                                .ThenInclude(k => k.TypeOfWork)
                                .Where(k => k.ID == fileId).FirstOrDefault();

            return file == null ? null : _mapper.Map<DataContract.ProjectFile>(file);
        }

        
    }
}