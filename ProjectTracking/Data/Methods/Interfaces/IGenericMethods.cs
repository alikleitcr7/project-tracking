using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IEntity
    {
        int ID { get; set; }
    }
    public class Entity : IEntity
    {
        public int ID { get; set; }
    }
    public interface IGenericRepository<TEntity> where TEntity : IEntity
    {
        TEntity Create(TEntity entity);
        bool Delete(TEntity entity);
        void Delete(int id);
        bool Edit(TEntity entity);

        //read side (could be in separate Readonly Generic Repository)
        TEntity GetByID(int id);
        IEnumerable<TEntity> Filter();
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);

        //separate method SaveChanges can be helpful when using this pattern with UnitOfWork
        void SaveChanges();
    }

    public interface IGenericRepository<TEntity, CEntity>
        where TEntity : IEntity
        where CEntity : DataContract.IEntity
    {
        CEntity Create(CEntity entity);
        bool Delete(int id);
        CEntity Edit(CEntity entity);

        CEntity GetByID(int id);
        List<CEntity> GetAll();
        List<CEntity> GetAll(int page, int countPerPage, out int totalCount);
        void SaveChanges();
    }


}


