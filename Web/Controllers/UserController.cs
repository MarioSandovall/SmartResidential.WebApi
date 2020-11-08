using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model.Models;
using Model.Models.User;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/residential/{residentialId}/user")]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IResidentialManager _residentialManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public UserController(
            IUserManager userManager,
            IResidentialManager residentialManager,
            IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
            _residentialManager = residentialManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] int residentialId, [FromQuery] Parameter parameter)
        {
            var loggedUser = User.GetUserId();
            if (!await _userManager.IsAdminAsync(residentialId, loggedUser))
            {
                return Forbid(_localizer.GetValue(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
            }

            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            var users = await _userManager.GetAllAsync(residentialId, parameter);
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get([FromRoute] int residentialId, [FromRoute] int userId)
        {
            var loggedUser = User.GetUserId();
            if (!await _userManager.IsAdminAsync(residentialId, loggedUser))
            {
                return Forbid(_localizer.GetValue(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
            }

            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            if (!await _userManager.ExistsAsync(residentialId, userId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.UserNotFound));
            }

            var user = await _userManager.GetByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] int residentialId, [FromBody] UserToAdd user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var loggedUser = User.GetUserId();
            if (!await _userManager.IsAdminAsync(residentialId, loggedUser))
            {
                return Forbid(_localizer.GetValue(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
            }

            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            if (await _userManager.ExistsEmailAsync(residentialId, user.Email))
            {
                return BadRequest(_localizer.GetValue(LocalizationMessage.EmailAlreadyExists));
            }

            await _userManager.AddAsync(user);
            return NoContent();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update([FromRoute] int residentialId, [FromRoute] int userId, [FromBody] UserToUpdate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var loggedUser = User.GetUserId();
            if (!await _userManager.IsAdminAsync(residentialId, loggedUser))
            {
                return Forbid(_localizer.GetValue(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
            }

            if (!await _userManager.ExistsAsync(residentialId, userId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.UserNotFound));
            }

            if (await _userManager.ExistsEmailAsync(residentialId, user.Id, user.Email))
            {
                return BadRequest(_localizer.GetValue(LocalizationMessage.EmailAlreadyExists));
            }

            await _userManager.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] int residentialId, [FromRoute] int userId)
        {

            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            var loggedUser = User.GetUserId();
            if (!await _userManager.IsAdminAsync(residentialId, loggedUser))
            {
                return Forbid(_localizer.GetValue(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
            }

            if (!await _userManager.ExistsAsync(residentialId, userId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.UserNotFound));
            }

            await _userManager.DeleteAsync(userId);

            return NoContent();
        }

        [HttpGet("resend-invitation/{userId}")]
        public async Task<IActionResult> ResendInvitation([FromRoute] int residentialId, [FromRoute] int userId)
        {
            var loggedUser = User.GetUserId();
            if (!await _userManager.IsAdminAsync(residentialId, loggedUser))
            {
                return Forbid(_localizer.GetValue(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
            }

            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            if (!await _userManager.ExistsAsync(residentialId, userId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.UserNotFound));
            }

            await _userManager.ResendInvitationAsync(userId);
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromRoute] int residentialId, [FromQuery] string name)
        {
            var loggedUser = User.GetUserId();
            if (!await _userManager.IsAdminAsync(residentialId, loggedUser))
            {
                return Forbid(_localizer.GetValue(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
            }

            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            var users = await _userManager.SearchAsync(residentialId, name);

            return Ok(new { users });
        }
    }
}