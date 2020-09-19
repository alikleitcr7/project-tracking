using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;

namespace ProjectTracking.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesMethods _categoriesMethods;

        public CategoriesController(ICategoriesMethods categoriesMethods)
        {
            _categoriesMethods = categoriesMethods;
        }

        [HttpGet]
        public JsonResult GetById(int id)
        {
            return Json(_categoriesMethods.GetById(id));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return Json(_categoriesMethods.GetAll());
        }

        [HttpPost]
        public JsonResult Add([FromBody]Category category)
        {
            return Json(_categoriesMethods.Add(category));
        }

        [HttpPut]
        public JsonResult Update([FromBody]Category category)
        {
            return Json(_categoriesMethods.Update(category));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            return Json(_categoriesMethods.Delete(id));
        }

    }
}