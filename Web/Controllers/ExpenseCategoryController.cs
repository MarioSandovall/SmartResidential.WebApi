using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model.Models;
using Model.Models.ExpenseCategory;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/residential/{residentialId}/expense-category")]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IResidentialManager _residentialManager;
        private readonly IExpenseCategoryManager _categoryManager;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public ExpenseCategoryController(
            IUserManager userManager,
            IResidentialManager residentialManager,
            IExpenseCategoryManager categoryManager,
            IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
            _categoryManager = categoryManager;
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

            var categories = await _categoryManager.GetAllAsync(residentialId, parameter);
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> Get([FromRoute] int residentialId, [FromRoute] int categoryId)
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

            if (!await _categoryManager.ExistsAsync(residentialId, categoryId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.CategoryNotFound));
            }

            var category = await _categoryManager.GetByIdAsync(categoryId);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] int residentialId, [FromBody] ExpenseCategoryToAdd category)
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

            if (await _categoryManager.ExistsAsync(residentialId, category.Name))
            {
                return BadRequest(_localizer.GetValue(LocalizationMessage.CategoryNameAlreadyExists));
            }

            await _categoryManager.AddAsync(category);
            return NoContent();
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Update([FromRoute] int residentialId, [FromRoute] int categoryId, [FromBody] ExpenseCategoryToUpdate category)
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

            if (!await _categoryManager.ExistsAsync(residentialId, categoryId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.CategoryNotFound));
            }

            if (await _categoryManager.ExistsAsync(residentialId, category.Id, category.Name))
            {
                return BadRequest(_localizer.GetValue(LocalizationMessage.EmailAlreadyExists));
            }

            await _categoryManager.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete([FromRoute] int residentialId, [FromRoute] int categoryId)
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

            if (!await _categoryManager.ExistsAsync(residentialId, categoryId))
            {
                return NotFound(_localizer.GetValue(LocalizationMessage.CategoryNotFound));
            }

            await _categoryManager.DeleteAsync(categoryId);

            return NoContent();
        }
    }
}