using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model.Models;
using Model.Models.House;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/residential/{residentialId}/house")]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    public class HouseController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IHouseManager _houseManager;
        private readonly IResidentialManager _residentialManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public HouseController(
            IUserManager userManager,
            IHouseManager houseManager,
            IResidentialManager residentialManager,
            IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
            _houseManager = houseManager;
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

            var houses = await _houseManager.GetAllAsync(residentialId, parameter);
            return Ok(houses);
        }

        [HttpGet("{houseId}")]
        public async Task<IActionResult> Get([FromRoute] int residentialId, [FromRoute] int houseId)
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

            if (!await _houseManager.ExistsAsync(residentialId, houseId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.HouseNotFound));
            }

            var house = await _houseManager.GetByIdAsync(houseId);
            return Ok(house);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] int residentialId, [FromBody] HouseToAdd house)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _residentialManager.ExistsAsync(residentialId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.ResidentialNotFound));
            }

            var loggedUser = User.GetUserId();
            if (!await _userManager.IsAdminAsync(residentialId, loggedUser))
            {
                return Forbid(_localizer.GetValue(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
            }

            if (await _houseManager.ExistsAsync(residentialId, house.Name))
            {
                return BadRequest(_localizer.GetValue(LocalizationMessage.NameOfHouseAlreadyExists));
            }

            await _houseManager.AddAsync(house);
            return NoContent();
        }

        [HttpPut("{houseId}")]
        public async Task<IActionResult> Update([FromRoute] int residentialId, [FromRoute] int houseId, [FromBody] HouseToUpdate house)
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

            if (!await _houseManager.ExistsAsync(residentialId, houseId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.HouseNotFound));
            }

            if (await _houseManager.ExistsAsync(residentialId, house.Id, house.Name))
            {
                return BadRequest(_localizer.GetValue(LocalizationMessage.NameOfHouseAlreadyExists));
            }

            await _houseManager.UpdateAsync(house);
            return NoContent();
        }

        [HttpDelete("{houseId}")]
        public async Task<IActionResult> Delete([FromRoute] int residentialId, [FromRoute] int houseId)
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

            if (!await _houseManager.ExistsAsync(residentialId, houseId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.HouseNotFound));
            }

            await _houseManager.DeleteAsync(houseId);

            return NoContent();
        }

    }
}