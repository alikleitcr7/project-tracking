using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Data.Methods;
using System.Data;

namespace ProjectTracking.Controllers
{
    public class InventoryProjectsController : Controller
    {
        private readonly IInventoryProjectsMethods _inventoryProjectsMethods;
        //private readonly IInventoryTypesMethods _inventoryTypesMethods;
        //private readonly IInventoryStatusesMethods _inventoryStatusesMethods;
        //private readonly IUpdateFrequenciesMethods _updateFrequenciesMethods;
        //private readonly IPublishingChannelsMethods _publishingChannelsMethods;
        private readonly IUserMethods _users;

        public InventoryProjectsController(IInventoryProjectsMethods inventoryProjectsMethods, IUserMethods users)
        {
            _inventoryProjectsMethods = inventoryProjectsMethods;
            _users = users;
            //_inventoryTypesMethods = inventoryTypesMethods;
            //_inventoryStatusesMethods = inventoryStatusesMethods;
            //_updateFrequenciesMethods = updateFrequenciesMethods;
            //_publishingChannelsMethods = publishingChannelsMethods;
        }

        public IActionResult Index()
        {
            return View();
        }

        public class AddProjectModel
        {
            public string Title { get; set; }
            //public string Description { get; set; }

            //public DateTime? DatePublished { get; set; }

            //public int? InventoryTypeId { get; set; }
            //public int? InventoryStatusId { get; set; }
            //public int? CountryId { get; set; }
            //public int? UpdateFrequencyId { get; set; }
            //public int? PublishingChanneld { get; set; }
        }

        public DataContract.InventoryProject Add([FromBody]DataContract.InventoryProject project)
        {

            return _inventoryProjectsMethods.Add(project);
        }

        public object Search([FromBody]DataContract.InventoryProjectFilter filter)
        {

            var records = _inventoryProjectsMethods.Search(filter, out int totalCount);

            return new
            {
                records,
                totalCount
            };

        }

        public FileResult AllToExcel([FromQuery]string selectedColumns)
        {
            if (string.IsNullOrEmpty(selectedColumns))
            {
                throw new Exception("Empty");
            }

            List<string> props = selectedColumns.Split(",").ToList();

            var data = _inventoryProjectsMethods.GetAll(0, 1000, out _);

            string fileName = $"Inventory_{DateTime.Now.ToString("ddMMyyyHHmmss")}.";

            DataTable dt = Data.Shared.ToDataTable(data, props);

            byte[] fileContents = Data.Shared.ExportToCSV(dt);

            return File(fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: fileName + ".xlsx");
        }

        public FileResult FilteredToExcel([FromBody]DataContract.InventoryProjectFilter filter)
        {
            //if (string.IsNullOrEmpty(selectedColumns))
            //{
            //    throw new Exception("Empty");
            //}

            filter.page = 0;
            filter.countPerPage = 10000;

            //List<string> props = new List<string>() { "Title" };
            List<string> props = filter.selectedColumns.Split(",").ToList();

            var data = _inventoryProjectsMethods.Search(filter, out _);

            string fileName = $"Inventory_{DateTime.Now.ToString("ddMMyyyHHmmss")}.";

            DataTable dt = Data.Shared.ToDataTable(data, props);

            byte[] fileContents = Data.Shared.ExportToCSV(dt);

            return File(fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: fileName + ".xlsx");
        }

        public List<DataContract.User> GetUsers()
        {
            return _users.GetAllUsers();
        }

        public bool Delete(int id)
        {
            return _inventoryProjectsMethods.Delete(id);
        }

        public DataContract.InventoryProject Get(int id)
        {
            return _inventoryProjectsMethods.Get(id);
        }

        public DataContract.InventoryProject Update([FromBody]DataContract.InventoryProject project)
        {
            return _inventoryProjectsMethods.Update(project);
        }

        public object GetAll(int page, int countPerPage)
        {
            var records = _inventoryProjectsMethods.GetAll(page, countPerPage, out int totalCount);

            return new
            {
                records,
                totalCount
            };
        }


    }
}