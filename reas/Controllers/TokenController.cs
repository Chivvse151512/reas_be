
using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reas.Services;
using service;

namespace reas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userContext;
        private readonly ITokenService _tokenService;
        public TokenController(IUserService userService
            , ITokenService tokenService)
        {
            this._userContext = userService ?? throw new ArgumentNullException(nameof(userService));
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        // Làm mới token
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenRefreshRequestModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            
            var user = _userContext.FindByUserName(username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            
            // Lưu lại token mới
            user.RefreshToken = newRefreshToken;
            _userContext.Update(user);

            return Ok(new
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        // Huỷ token
        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;

            var user = _userContext.FindByUserName(username);
            
            if (user == null)
                return BadRequest();

            user.RefreshToken = null;
            _userContext.Update(user);

            return NoContent();
        }
    }
}
