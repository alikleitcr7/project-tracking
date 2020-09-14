using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace ProjectTracking.Controllers
{
    public class ProjectFilesController : Controller
    {
        private readonly IProjectFilesMethods _projectFilesMethods;
        private readonly IMapper _mapper;

        public ProjectFilesController(IProjectFilesMethods projectMethods, IMapper mapper)
        {
            _projectFilesMethods = projectMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.ProjectFile Create(string name, int projectId)
        {
            if (name != null)
            {
                name = name.Trim();
            }

            var dbProjectFile = _projectFilesMethods.Create(new Data.DataSets.ProjectReference()
            {
                Name = name
                ,
                ProjectId = projectId
            });

            if (dbProjectFile != null)
            {
                return _mapper.Map<DataContract.ProjectFile>(dbProjectFile);
            }

            return default;
        }

        public bool Delete(int id)
        {
            var dbProjectFile = _projectFilesMethods.GetByID(id);

            if (dbProjectFile != null)
            {
                return _projectFilesMethods.Delete(dbProjectFile);
            }

            return false;
        }


        public bool Update(int id, string name)
        {
            if (name!=null)
            {
                name = name.Trim();
            }
            var dbProjectFile = _projectFilesMethods.GetByID(id);

            if (dbProjectFile != null)
            {
                dbProjectFile.Name = name;
                return _projectFilesMethods.Edit(dbProjectFile);
            }

            return false;
        }

        public List<DataContract.ProjectFile> GetAll()
        {
            var dbProjectFiles = _projectFilesMethods.Filter().ToList();

            if (dbProjectFiles != null)
            {
                return dbProjectFiles.Select(k => _mapper.Map<DataContract.ProjectFile>(k)).ToList();
            }

            return new List<DataContract.ProjectFile>();
        }

        public List<DataContract.ProjectFile> GetByProject(int projectId)
        {
            var dbProjectFiles = _projectFilesMethods.Filter(c => c.ProjectId == projectId).ToList();

            if (dbProjectFiles != null)
            {
                return dbProjectFiles.Select(k => _mapper.Map<DataContract.ProjectFile>(k)).ToList();
            }

            return new List<DataContract.ProjectFile>();
        }

        public List<DataContract.ProjectFile> Search([FromQuery]string keyword, int? projectId)
        {
            var dbProjectFiles = _projectFilesMethods.Search(keyword, projectId);

            if (dbProjectFiles != null)
            {
                return dbProjectFiles.Select(k => _mapper.Map<DataContract.ProjectFile>(k)).ToList();
            }

            return new List<DataContract.ProjectFile>();
        }
    }
}