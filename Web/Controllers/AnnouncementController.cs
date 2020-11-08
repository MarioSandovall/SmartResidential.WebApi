using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model.Models.Announcement;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/residential/{residentialId}/announcement")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IResidentialManager _residentialManager;
        private readonly IAnnouncementManager _announcementManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public AnnouncementController(
            IUserManager userManager,
            IResidentialManager residentialManager,
            IAnnouncementManager announcementManager,
            IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
            _residentialManager = residentialManager;
            _announcementManager = announcementManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromRoute] int residentialId)
        {
            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            var announcements = await _announcementManager.GetAllAsync(residentialId);
            return Ok(new { announcements });
        }

        [HttpGet("{announcementId}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] int residentialId, [FromRoute] int announcementId)
        {
            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            if (!await _announcementManager.ExistsAsync(residentialId, announcementId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.AnnouncementNotFound));
            }

            var category = await _announcementManager.GetByIdAsync(announcementId);
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> Add([FromRoute] int residentialId, [FromBody] AnnouncementToAdd announcement)
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

            announcement.UserId = loggedUser;
            await _announcementManager.AddAsync(announcement);

            return NoContent();
        }

        [HttpPut("{announcementId}")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> Update([FromRoute] int residentialId, [FromRoute] int announcementId, [FromBody] AnnouncementToUpdate announcement)
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

            if (!await _announcementManager.ExistsAsync(residentialId, announcementId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.AnnouncementNotFound));
            }

            announcement.UserId = loggedUser;
            await _announcementManager.UpdateAsync(announcement);

            return NoContent();
        }

        [HttpDelete("{announcementId}")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> Delete([FromRoute] int residentialId, [FromRoute] int announcementId)
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

            if (!await _announcementManager.ExistsAsync(residentialId, announcementId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.AnnouncementNotFound));
            }

            await _announcementManager.DeleteAsync(announcementId);

            return NoContent();
        }

        [HttpGet("resend-email/{announcementId}")]
        [Authorize(Roles = AuthorizationRoles.Admin)]
        public async Task<IActionResult> ResendEmail([FromRoute] int residentialId, [FromRoute] int announcementId)
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

            if (!await _announcementManager.ExistsAsync(residentialId, announcementId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.AnnouncementNotFound));
            }

            await _announcementManager.ResendEmailAsync(announcementId);

            return Ok();
        }
    }
}
