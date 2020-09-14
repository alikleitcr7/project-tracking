using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models;

namespace ProjectTracking.Controllers
{
    public class HolidaysController : Controller
    {
        private readonly IHolidaysMethods _holidays;

        public HolidaysController(IHolidaysMethods holidays)
        {
            _holidays = holidays;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ClientResponseModel<Holiday> Create([FromBody]HolidayAddModel model)
        {
            ClientResponseModel<Holiday> response = new ClientResponseModel<Holiday>();

            try
            {
                response.Record = _holidays.Create(new DataContract.Holiday()
                {
                    Title = model.title,
                    Date = model.date,
                    Note = model.note
                });


                response.Message = "Holiday was added successfully!";
            }
            catch (ClientException clientEx)
            {
                response.SetClientException(clientEx);
            }
            catch (Exception ex)
            {
                response.SetInternalException(ex);
            }

            return response;
        }

        [HttpDelete]
        public ClientResponseModel<bool> Delete(int id)
        {
            ClientResponseModel<bool> response = new ClientResponseModel<bool>();

            try
            {
                response.SetSuccessStatus(_holidays.Delete(id));
            }
            catch (Exception ex)
            {
                response.SetInternalException(ex);
            }

            return response;
        }

        [HttpPut]
        public ClientResponseModel<Holiday> Update([FromBody]HolidayUpdateModel model)
        {
            ClientResponseModel<Holiday> response = new ClientResponseModel<Holiday>();

            try
            {
                response.Record = _holidays.Edit(new Holiday()
                {
                    ID = model.id,
                    Title = model.title,
                    Note = model.note,
                    Date = model.date
                });

                response.SetSuccessStatus(true, "Holiday was udpated successfully!");
            }
            catch (ClientException clientEx)
            {
                response.SetClientException(clientEx);
            }
            catch (Exception ex)
            {
                response.SetInternalException(ex);
            }

            return response;
        }

        [HttpGet]
        public ClientResponseModel<List<Holiday>> GetAll()
        {
            ClientResponseModel<List<Holiday>> model = new ClientResponseModel<List<Holiday>>();

            try
            {
                model.Record = _holidays.GetAll();
            }
            catch (Exception ex)
            {
                model.SetInternalException(ex);
            }

            return model;
        }

        [HttpGet]
        public ClientResponseModel<List<Holiday>> GetAllPaged(int page, int countPerPage)
        {
            ClientResponseModel<List<Holiday>> model = new ClientResponseModel<List<Holiday>>();

            try
            {
                model.Record = _holidays.GetAll(page, countPerPage, out int totalCount);
                model.ExtraData = new { totalCount };
            }
            catch (Exception ex)
            {
                model.SetInternalException(ex);
            }

            return model;
        }


        #region Models
        public class HolidayAddModel
        {
            public string title { get; set; }
            public string note { get; set; }
            public DateTime date { get; set; }
        }

        public class HolidayUpdateModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public string note { get; set; }
            public DateTime date { get; set; }
        }
        #endregion

    }
}