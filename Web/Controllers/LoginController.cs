using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model.Models.Login;
using Service.Interfaces;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserLoginManager _manager;
        private readonly ILoginService _loginService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public LoginController(
            IUserLoginManager manager,
            ILoginService tokenservice,
            IStringLocalizer<SharedResource> localizer)
        {
            _manager = manager;
            _localizer = localizer;
            _loginService = tokenservice;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _manager.ExistsAsync(model.Email, model.Password))
            {
                return BadRequest(_localizer.GetValue(LocalizationMessage.EmailOrPasswordIsIncorrect));
            }

            var user = await _manager.GetUserAsync(model.Email, model.Password);
            var token = _loginService.BuildToken(user);

            return Ok(token);
        }

        [HttpGet("user")]
        [Authorize(Policy = AuthorizationRoles.PolicyName)]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.GetUserId();
            var user = await _manager.GetUserAsync(userId);

            return Ok(user);
        }

    }
}