using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/residential/{residentialId}/user/role")]
    [Authorize(Roles = AuthorizationRoles.SuperAdminOrAdmin)]
    public class UserRoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;
        private readonly IResidentialManager _residentialManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public UserRoleController(
            IRoleManager roleManager,
            IResidentialManager residentialManager,
            IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            _roleManager = roleManager;
            _residentialManager = residentialManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int residentialId)
        {
            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            var roles = await _roleManager.GetResidentialRolesAsync();
            return Ok(new { roles });
        }
    }
}