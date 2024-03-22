using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Org.BouncyCastle.Utilities;
using reas.Helpers;
using reas.Model;
using reas.Services;
using service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace reas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEMailSenderService _eMailSenderService;
        private readonly ITokenService _tokenService;

        public UserController(
            IUserService userService,
            IConfiguration configuration,
            IEMailSenderService eMailSenderService,
            ITokenService tokenService
         )
        {
            _userService = userService;
            _configuration = configuration;
            _eMailSenderService = eMailSenderService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize]
        [EnableQuery]
        [Route("get-list")]
        public ActionResult<List<User>> GetList()
        {
            var users = _userService.Get();
            return Ok(users);
        }

        [HttpGet]
        [Authorize]
        [Route("get/{userId}")]
        public ActionResult<User> Get(int userId)
        {
            var user = _userService.Get(userId);

            if (user == null)
            {
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    new ResponseModel { Status = "Error", Message = "User not found!" });
            }

            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        [Route("get/current")]
        public ActionResult<User> Get()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = _userService.Get(int.Parse(userId));

            if (user == null)
            {
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    new ResponseModel { Status = "Error", Message = "User not found!" });
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            var user = _userService.FindByUserName(model.UserName);

            if (user == null
                || !_userService.CheckPassword(user, model.Password))
                return Unauthorized();

            if (user.Status <= 0)
            {
                return Ok(new ResponseModel
                {
                    Status = "Error",
                    Message = "User Baned !"
                });
            }

            var role = _userService.GetRole(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role.Name)
            };

            var accessToken = _tokenService.GenerateAccessToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // https://code-maze.com/using-refresh-tokens-in-asp-net-core-authentication/
            // Lưu lại thông tin refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _userService.Update(user);

            return Ok(new
            {
                Role = user.RoleId,
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public IActionResult SignUp([FromBody] UserSignUpRequestModel model)
        {
            var userExists = _userService.FindByUserName(model.UserName);
            if (userExists != null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = "Error", Message = "User already exists!" });
            }

            User user = new()
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                FullName = model.FullName,
                Address = model.Address,
                Phone = model.Phone,
                Avatar = model.Avatar,
                RoleId = 2, // USER
                Status = 1, // Active
                CreatedAt = DateTime.UtcNow,
            };

            var result = _userService.Create(user);

            if (result.Id <= 0)
                return StatusCode(
                    StatusCodes.Status500InternalServerError
                    , new ResponseModel {
                        Status = "Error"
                        , Message = "User creation failed! Please check user details and try again."
                    });

            return Ok(new ResponseModel {
                Status = "Success"
                , Message = "User created successfully!"
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var user = _userService.FindByEmail(model.Email);
            
            if (user == null)
            {
                return Ok(new ResponseModel
                {
                    Status = "Error",
                    Message = "Email not found !"
                });
            }

            var token = GeneratePasswordResetToken(user);

            _eMailSenderService.SendEmail(user.Email
                , "[REAS] Forgot Password Reset Token"
                , string.Format("Your Facebook account recovery token : {0}", token));

            return Ok(new ResponseModel
            {
                Status = "Success",
                Message = "Reset Token Send !"
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password-confirm")]
        public IActionResult ForgotPasswordConfirm([FromBody] ForgotPasswordConfirmModel model)
        {
            var user = _userService.FindByEmail(model.Email);

            if (user == null)
            {
                return StatusCode(
                   StatusCodes.Status404NotFound,
                   new ResponseModel { Status = "Error", Message = "User not found !" });
            }

            var token = GeneratePasswordResetToken(user);

            if (token == model.Token)
            {
                user.Password = model.NewPassword;
                _userService.Update(user);

                return Ok(new ResponseModel
                {
                    Status = "Success",
                    Message = "Password Reset !"
                });
            }

            return Ok(new ResponseModel
            {
                Status = "Error",
                Message = "Can't reset user password !"
            });
        }

        [HttpPost]
        [Authorize]
        [Route("change-password")]
        public IActionResult ChangePassword(ChangePasswordRequestModel model)
        {
            var user = _userService.FindByUserName(model.UserName);

            if (user == null)
            {
                return StatusCode(
                   StatusCodes.Status404NotFound,
                   new ResponseModel { Status = "Error", Message = "User not found !" });
            }

            if (model.OldPassword == user.Password)
            {
                user.Password = model.NewPassword;
                _userService.Update(user);

                return Ok(new ResponseModel
                {
                    Status = "Success",
                    Message = "Password changed !"
                });
            }

            return Ok(new ResponseModel
            {
                Status = "Error",
                Message = "Can't change user password !"
            });
        }

        [HttpPost]
        [Authorize]
        [Route("update-profile")]
        public IActionResult UpdateProfile([FromBody] UserUpdateProfileRequestModel model)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var user = _userService.Get(int.Parse(userId));

            if (user == null)
            {
                return StatusCode(
                  StatusCodes.Status404NotFound,
                  new ResponseModel { Status = "Error", Message = "User not found !" });
            }

            user.Email = model.Email;
            user.FullName = model.FullName;
            user.Address = model.Address;
            user.Phone = model.Phone;
            user.Avatar = model.Avatar;

            var result = _userService.Update(user);

            if (!result)
                return StatusCode(
                    StatusCodes.Status500InternalServerError
                    , new ResponseModel
                    {
                        Status = "Error",
                        Message = "User update profile failed! Please check user details and try again."
                    });

            return Ok(new ResponseModel
            {
                Status = "Success",
                Message = "User update profile successfully!"
            });
        }

        [HttpPost]
        [Authorize]
        [Route("ban/{userId}")]
        public IActionResult BanUser(int userId)
        {
            var result = _userService.Ban(userId);

            if (!result)
                return StatusCode(
                    StatusCodes.Status500InternalServerError
                    , new ResponseModel
                    {
                        Status = "Error"
                        ,
                        Message = "User ban failed! Please check user details and try again."
                    });

            return Ok(new ResponseModel
            {
                Status = "Success"
                ,
                Message = "User ban successfully!"
            });
        }

        [HttpPost]
        [Authorize]
        [Route("delete/{userId}")]
        public IActionResult Delete(int userId)
        {
            var result = _userService.Delete(userId);

            if (!result)
                return StatusCode(
                    StatusCodes.Status500InternalServerError
                    , new ResponseModel
                    {
                        Status = "Error",
                        Message = "User delete failed! Please check user details and try again."
                    });

            return Ok(new ResponseModel
            {
                Status = "Success",
                Message = "User delete successfully!"
            });
        }

        #region Helpers
        private string GeneratePasswordResetToken(User user)
        {
            return CryptoHelpers.CreateMD5(user.Email + user.Password);
        }
        #endregion
    }
}
