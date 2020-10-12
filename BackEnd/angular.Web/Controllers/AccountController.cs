using angular.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using reactiveFormWeb.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace angular.Web.Controllers
{

    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private ApplicationDbContext _context { get; set; }

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
            _context = context;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        return BuildToken(user, roles);
                    }
                    else
                    {
                        var payload = new { errors = result.Errors.Select(x => x.Description).ToArray() };
                        return BadRequest(payload);
                    }
                }
                catch(Exception ex)
                {
                    var e = ex;
                    throw;
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByNameAsync(userInfo.Email);
                        var roles = await _userManager.GetRolesAsync(user);
                        return BuildToken(user, roles);
                    }
                    else
                    {
                        ModelState.AddModelError("errors", "Invalid user or password.");
                        return BadRequest(ModelState);
                    }
                }
                catch (Exception ex)
                {
                    var e = ex;
                    throw;
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(ApplicationUser user, IList<string> roles)
        {
            var rolesStr = JsonConvert.SerializeObject(roles);
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Roles", rolesStr),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Super_secret_key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(7);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration
            });

        }
    }

}
