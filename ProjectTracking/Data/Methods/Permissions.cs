using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;

namespace ProjectTracking.Data.Methods
{
    public class Permissions : IPermissionsMethods
    {
        private ApplicationDbContext _db;
        private IMapper _mapper;
        public Permissions(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<Permission> Get()
        {
            var PermissionsDb = _db.Permissions.ToList();
            return PermissionsDb.Select(_mapper.Map<DataContract.Permission>).ToList();
        }
        public Permission Add(Permission permission)
        {
            if (permission == null)
            {
                throw new NullReferenceException();
            }

            // check if permission exist 

            if (_db.Permissions.Any(k => k.Title.Equals(permission.Title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ClientException("Permission exist with the same name");
            }

            var dbPermission = _mapper.Map<DataContract.Permission, DataSets.Permission>(permission);

            _db.Permissions.Add(dbPermission);

            _db.SaveChanges();

            return _mapper.Map<Permission>(dbPermission);
        }
        public bool Update(Permission permission)
        {
            if (permission == null)
                throw new NullReferenceException();

            var permissionInDb = _db.Permissions.FirstOrDefault(c => c.ID == permission.ID);

            if (permissionInDb == null)
                throw new ClientException("Permission Not Found");

            if (_db.Permissions.Any(k => k.Title.Equals(permission.Title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ClientException("Permission exist with the same name");
            }

            permissionInDb.Title = permission.Title;
            permissionInDb.Description = permission.Description;

            return _db.SaveChanges() == 1;
        }
        public bool Delete(int id)
        {
            var PermissionInDb = _db.Permissions.FirstOrDefault(c => c.ID == id);

            if (PermissionInDb == null)
                return false;

            _db.Permissions.Remove(PermissionInDb);

            return _db.SaveChanges() > 0;
        }

    }
}
