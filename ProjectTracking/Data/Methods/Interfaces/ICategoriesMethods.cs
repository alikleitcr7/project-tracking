using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ICategoriesMethods
    {
        Category Add(Category Companydto);
        bool Delete(int id);
        List<Category> GetAll();
        //Category Edit(int id, Category Companydto);
        Category GetById(int id);
        Category Update(Category category);
    }
}