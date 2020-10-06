using eMeeting.API.Data;
using eMeeting.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace eMeeting.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors(origins: "http://emeeting.dsolit.com", headers: "*", methods: "*")]
    public class AppUserController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        //private SignInManager<AppUser> _singInManager;
        private readonly ApplicationSettings _appSettings;

        public AppUserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
           //_singInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var token = "";
            var success = 0;
            var message = "Username or password is incorrect.";

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(securityToken);
                success = 1;
                message = "Successfully Logged In.";
                return Ok(new { success, message, token });
            }
            else
                return BadRequest(new { success, message, token });
        }

        [HttpPost("Logout")]
        [Authorize]
        public ActionResult Logout()
        {
            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        {
            var appUser = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(appUser, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
