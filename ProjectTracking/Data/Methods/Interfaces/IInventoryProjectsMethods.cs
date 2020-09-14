using System.Collections.Generic;
using ProjectTracking.DataContract;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IInventoryProjectsMethods
    {
        InventoryProject Add(InventoryProject inventory);
        bool Delete(int id);
        InventoryProject Get(int id);
        List<InventoryProject> GetAll(int page, int countPerPage, out int totalCount);
        List<InventoryProject> Search(InventoryProjectFilter filter, out int totalCount);
        InventoryProject Update(InventoryProject inventory);
    }
}