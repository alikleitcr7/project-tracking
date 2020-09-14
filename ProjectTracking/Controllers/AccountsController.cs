using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectTracking.AppStart;
using ProjectTracking.Managers;
using ProjectTracking.Models.Admin;

namespace ProjectTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowOrigin")]
    public class AccountsController : ControllerBase
    {
        private readonly IJwtManager _jwtManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(IJwtManager jwtManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _jwtManager = jwtManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            IActionResult response = Unauthorized();

            var user = await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.UserName);
            }

            bool validPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!validPassword)
            {
                return response;
            }

            var roles = await _userManager.GetRolesAsync(user);

            string role = roles.Count > 0 ? roles[0] : Policies.User;

            return Ok(new
            {
                token = _jwtManager.GenerateToken(user.UserName, role),
            });
        }

        [HttpGet, Route("GetUser")]
        [Authorize]
        public IActionResult GetUser(string token)
        {
            try
            {
                var auth = User.Identity.IsAuthenticated;

                var claims = _jwtManager.GetPrincipal(token);
                return Ok(new { claims = claims.Claims.ToList().Select(k => new { key = k.Type, value = k.Value }).ToList() });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}