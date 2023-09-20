using MeetingSchedulingApp.Application.Implimentation;
using MeetingSchedulingApp.Application.Interfaces;
using MeetingSchedulingApp.Application.SmtpService;
using MeetingSchedulingApp.Helper;
using MeetingSchedulingApp.Model.DatabaseModel;
using MeetingSchedulingApp.Model.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingSchedulingApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructor
        public UserController(ILoginService loginservice, IUserService userService,IEmailService emailService)
        {
            _loginService = loginservice;
            _userService = userService;
            _emailService = emailService;
        }
        #endregion

        #region Method
        #region Get
        [HttpGet]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {

                var result = await _userService.GetAll();
                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Users>() { IsSuccess = true, Message = "Api Execute Successfully", Results = result });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Users>() { IsSuccess = false, Message = ex.Message });
            }
        }
        #endregion
        #region Post

        [HttpPost]
        public async Task<IActionResult> Post(Users users)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                users.CreatedAt= DateTime.Now;
                users.UpdatedAt = DateTime.Now;
                var user = await _userService.Create(users);
                var password = Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));
                byte[] salt;
                var hash = PasswordHelper.HashPasword(password, out salt);
                var hexstring = Convert.ToHexString(salt);
                var login = new Login() { IsActive = true,User_Id = user.Id, HashPassword= hash,SaltPassword=hexstring,CreatedAt = DateTime.Now,UpdatedAt= DateTime.Now};
                var result = await _loginService.Create(login);
                var name = $"{user.FirstName} {user.LastName}";
                var body = "<!DOCTYPE html><html><head><style>   body {font - family: Arial, sans-serif;    background-color: #f4f4f4;    margin: 0;    padding: 0;  }  .container {max - width: 600px;    margin: 0 auto;    padding: 20px;    background-color: #ffffff;    border-radius: 5px;    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);  }  .header {text - align: center;    margin-bottom: 20px;  }  .message {padding: 20px;    background-color: #f9f9f9;    border-radius: 5px;  }  .button {display: inline-block;    padding: 10px 20px;    background-color: #007bff;    color: #ffffff;    text-decoration: none;    border-radius: 5px;  }</style></head><body><div class=" + "container" + ">  <div class=" + "header" + ">    <h1>Your Login Password</h1>  </div>  <div class=" + "message" + ">    <p>Hello <strong>" +name + ",</strong></p>   <p>Here is your  Email:</p><p><strong>" + user.Email + "</strong></p>   <p>Here is your  password:</p>    <p><strong>" + password + "</strong></p>    <p>Please make sure to change your password after logging in for security reasons.</p>    <p>If you have any questions or concerns, feel free to contact our support team.</p>    <p>Thank you!</p>  </div>  <div class=" + "footer" + ">    <p>This is an automated email. Please do not reply.</p>  </div></div></body></html>";
                var email = await _emailService.SendEmailAsync(new EmailMessage() { Body = body, IsHtml = true, ReceiversEmail = user.Email, Subject = "Login Password" });

                return StatusCode(StatusCodes.Status200OK, new StatusResponse<Users>() { IsSuccess= true , Message="User Successfully Register",Result = user });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse<Users>() { IsSuccess = false, Message = "Failed to registe user"});

            }
        }
        #endregion
        private string GeneratePassword()
        {
            int PasswordLength = 6;
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";


            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < PasswordLength; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;
            }
            return NewPassword;
        }
        #endregion

    }
}
