using AutoMapper;
using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTracking.Data.Methods
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context) => _context = context;

        public TEntity Create(TEntity entity)
        {
            var pendingAdd = _context.Set<TEntity>().Add(entity);

            if (_context.SaveChanges() > 0)
            {
                return pendingAdd.Entity;
            }

            return default;
        }

        public bool Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

            return _context.SaveChanges() > 0;
        }

        public void Delete(int id)
        {
            var entityToDelete = _context.Set<TEntity>().FirstOrDefault(e => e.ID == id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }
        }

        public bool Edit(TEntity entity)
        {
            var editedEntity = _context.Set<TEntity>().FirstOrDefault(e => e.ID == entity.ID);
            editedEntity = entity;

            return _context.SaveChanges() > 0;
        }

        public TEntity GetByID(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<TEntity> Filter()
        {
            return _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public void SaveChanges() => _context.SaveChanges();
    }
    public class GenericRepository<TEntity, CEntity> : IGenericRepository<TEntity, CEntity>
        where CEntity : class, DataContract.IEntity
        where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual CEntity Create(CEntity entity)
        {
            TEntity dbEntity = _mapper.Map<TEntity>(entity);

            var pendingAdd = _context.Set<TEntity>().Add(dbEntity);

            if (_context.SaveChanges() > 0)
            {
                return GetByID(dbEntity.ID);
            }

            return default;
        }

        protected virtual bool Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

            return _context.SaveChanges() > 0;
        }

        public virtual bool Delete(int id)
        {
            var entityToDelete = _context.Set<TEntity>().FirstOrDefault(e => e.ID == id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }

            return _context.SaveChanges() > 0;
        }

        protected virtual CEntity Edit(TEntity entity)
        {
            var editedEntity = _context.Set<TEntity>().FirstOrDefault(e => e.ID == entity.ID);
            editedEntity = entity;

            _context.SaveChanges();

            return GetByID(entity.ID);
        }

        public virtual CEntity Edit(CEntity entity)
        {
            TEntity dbEntity = _mapper.Map<TEntity>(entity);

            var editedEntity = _context.Set<TEntity>().FirstOrDefault(e => e.ID == entity.ID);
            editedEntity = dbEntity;

            _context.SaveChanges();

            return GetByID(entity.ID);
        }

        public virtual CEntity GetByID(int id)
        {
            TEntity dbEntity = _context.Set<TEntity>().FirstOrDefault(e => e.ID == id);

            if (dbEntity == null)
            {
                return null;
            }

            return _mapper.Map<CEntity>(dbEntity);
        }

        protected virtual IEnumerable<TEntity> Filter()
        {
            return _context.Set<TEntity>();
        }

        public virtual List<CEntity> GetAll(int page, int countPerPage, out int totalCount)
        {
            totalCount = Filter().Count();

            return Filter().ToList()
                           .Skip(page * countPerPage)
                           .Take(countPerPage)
                           .Select(_mapper.Map<CEntity>)
                           .ToList();
        }

        public virtual List<CEntity> GetAll()
        {
            return Filter().ToList()
                           .Select(_mapper.Map<CEntity>)
                           .ToList();
        }

        protected virtual IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public void SaveChanges() => _context.SaveChanges();


    }
}
