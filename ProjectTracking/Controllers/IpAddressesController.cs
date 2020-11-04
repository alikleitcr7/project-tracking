using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Exceptions;

namespace ProjectTracking.Controllers
{
    public class IpAddressesController : Controller
    {
        private readonly IIpAddressMethods _ipAddresses;

        public IpAddressesController(IIpAddressMethods ipAddresses)
        {
            _ipAddresses = ipAddresses;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUnlistedIps()
        {
            try
            {
                return Ok(_ipAddresses.UnListedIps());
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]IpAddressSaveModel model)
        {
            try
            {
                IpAddress ipAddress = _ipAddresses.Add(new IpAddress()
                {
                    Address = model.address,
                    Title = model.title
                });

                return Ok(ipAddress);
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult Delete(string address)
        {
            try
            {
                _ipAddresses.Delete(address);

                return Ok(true);
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody]IpAddressSaveModel model)
        {
            try
            {
                IpAddress ipAddress = _ipAddresses.Update(new IpAddress()
                {
                    Address = model.address,
                    Title = model.title
                });

                return Ok(ipAddress);
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Save([FromBody]IpAddressSaveModel model)
        {
            try
            {
                IpAddress ipAddress = _ipAddresses.Save(new IpAddress()
                {
                    Address = model.address,
                    Title = model.title
                });

                return Ok(ipAddress);
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dbIpAddresses = _ipAddresses.GetAll();

                return Ok(dbIpAddresses);
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetListed()
        {
            try
            {
                var dbIpAddresses = _ipAddresses.GetListed();

                return Ok(dbIpAddresses);
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        public class IpAddressSaveModel
        {
            public string address { get; set; }
            public string title { get; set; }
        }
    }
}