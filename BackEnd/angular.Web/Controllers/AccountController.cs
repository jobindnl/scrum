using angular.Web.Models;
using angular.Web.Models.DTO;
using EmailService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
using System.Web;

namespace angular.Web.Controllers
{

    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        private ApplicationDbContext _context { get; set; }

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
            _context = context;
            _emailSender = emailSender;
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

        [Route("ChangePassword")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] PwdChange model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUserId = User.FindFirst("Id").Value;
                    var user = await _userManager.FindByIdAsync(currentUserId);
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPwd);
                    if (result.Succeeded)
                    {
                        await _signInManager.RefreshSignInAsync(user);
                        var roles = await _userManager.GetRolesAsync(user);
                        return BuildToken(user, roles);
                    }
                    else
                    {
                        var payload = new { errors = result.Errors.Select(x => x.Description).ToArray() };
                        return BadRequest(payload);
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

        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPasswordModel)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null) {
                var payload = new { errors = new string[] { "This email does not belong to a user" } };
                return Conflict(payload);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = $"{Request.Scheme}://{Request.Host}/verify-token-reset-password?token={token}&email={user.Email}";
            var message = new Message(new string[] { forgotPasswordModel.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);
            return NoContent();

        }

        [Route("verify-token-reset-password")]
        [HttpPost]
        public async Task<IActionResult> VerifyTokenAndResetPassword([FromBody] ForgotPasswordToken ForgotPasswordToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(ForgotPasswordToken.Email);
            if (user == null)
            {
                var payload = new { errors = new string[] { "This email does not belong to a user" } };
                return Conflict(payload);
            }
            var code = ForgotPasswordToken.Token.Replace(" ", "+");
            var identityResult = await _userManager.ResetPasswordAsync(user, code, ForgotPasswordToken.newPwd);
            if (identityResult.Succeeded)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, ForgotPasswordToken.newPwd, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return BuildToken(user, roles);
                }
                else
                {
                    ModelState.AddModelError("errors", "Invalid user or password.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                var payload = new { errors = identityResult.Errors.Select(x => x.Description).ToArray() };
                return Conflict(payload);
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
