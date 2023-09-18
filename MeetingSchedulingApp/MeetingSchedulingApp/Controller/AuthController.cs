using MeetingSchedulingApp.Application.Interfaces;
using MeetingSchedulingApp.Model.ApiModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MeetingSchedulingApp.Helper;
using MeetingSchedulingApp.Model.ResponseModel;
using MeetingSchedulingApp.Model.DatabaseModel;

namespace MeetingSchedulingApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public AuthController(ILoginService loginservice,IUserService userService)
        {
            _loginService = loginservice;
            _userService = userService;
        }
        #endregion

        #region Method

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var logindata = await _loginService.GetByEmail(login.Email);

                if (logindata == null)
                {
                    return NotFound();
                }
                byte[] salt = Convert.FromHexString(logindata.SaltPassword);
                var check = PasswordHelper.VerifyPassword(login.Password, logindata.HashPassword, salt);
                if (check)
                {
                    return StatusCode(200 ,new AuthRespose() { isAuthenticated = true,Login = logindata});
                }
              
                return StatusCode(200, new AuthRespose() { isAuthenticated = false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(500, ex);
            }
        }
        #endregion

        #region ChangePassword
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody]PasswordChange passwordChange)
        {
            try
            {
                var login = await _loginService.GetById_Email(passwordChange.Email, passwordChange.Id);
                if(login == null)
                {
                    throw new Exception("User not found");
                }
                byte[] salt = Convert.FromHexString(login.SaltPassword);
                var check = PasswordHelper.VerifyPassword(passwordChange.OldPassword, login.HashPassword, salt);
                if (!check)
                {
                    throw new Exception("Invalid current Password");
                }
                byte[] Newsalt;
                var hash = PasswordHelper.HashPasword(passwordChange.NewPassword, out Newsalt);
                var hexstring = Convert.ToHexString(Newsalt);
                login.HashPassword = hash;
                login.SaltPassword = hexstring;
                login.UpdatedAt= DateTime.Now;
                var result = await _loginService.Update(login);
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Login>() { IsSuccess = true, Message="Password successfully changed",Result = result});

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Login>() { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion

        #endregion

    }
}
